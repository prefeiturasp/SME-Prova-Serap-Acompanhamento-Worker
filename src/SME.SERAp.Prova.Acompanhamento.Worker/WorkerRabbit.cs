using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SME.SERAp.Prova.Acompanhamento.Aplicacao;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Acompanhamento.Infra.Exceptions;
using SME.SERAp.Prova.Acompanhamento.Infra.Extensions;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using SME.SERAp.Prova.Acompanhamento.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static SME.SERAp.Prova.Acompanhamento.Infra.Services.ServicoLog;

namespace SME.SERAp.Prova.Acompanhamento.Worker
{
    public class WorkerRabbit : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly RabbitOptions rabbitOptions;
        private readonly ConnectionFactory connectionFactory;
        private readonly ILogger<WorkerRabbit> logger;
        private readonly IServicoTelemetria servicoTelemetria;
        private readonly IServicoLog servicoLog;
        private readonly IServicoMensageria servicoMensageria;

        private readonly Dictionary<string, ComandoRabbit> comandos;

        public WorkerRabbit(
            IServiceScopeFactory serviceScopeFactory,
            RabbitOptions rabbitOptions,
            ConnectionFactory connectionFactory,
            ILogger<WorkerRabbit> logger,
            IServicoTelemetria servicoTelemetria,
            IServicoLog servicoLog,
            IServicoMensageria servicoMensageria)
        {
            this.serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            this.rabbitOptions = rabbitOptions ?? throw new ArgumentNullException(nameof(rabbitOptions));
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.servicoTelemetria = servicoTelemetria ?? throw new ArgumentNullException(nameof(servicoTelemetria));
            this.servicoLog = servicoLog ?? throw new ArgumentNullException(nameof(servicoLog));
            this.servicoMensageria = servicoMensageria ?? throw new ArgumentNullException(nameof(servicoMensageria));
            
            comandos = new Dictionary<string, ComandoRabbit>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var conexaoRabbit = connectionFactory.CreateConnection();
            using var channel = conexaoRabbit.CreateModel();

            var props = channel.CreateBasicProperties();
            props.Persistent = true;

            channel.BasicQos(0, rabbitOptions.LimiteDeMensagensPorExecucao, false);

            channel.ExchangeDeclare(ExchangeRabbit.SerapEstudanteAcompanhamento, ExchangeType.Direct, true);
            channel.ExchangeDeclare(ExchangeRabbit.SerapEstudanteAcompanhamentoDeadLetter, ExchangeType.Direct, true);

            DeclararFilas(channel);

            RegistrarUseCases();

            await InicializaConsumer(channel, stoppingToken);
        }

        private void DeclararFilas(IModel channel)
        {
            foreach (var fila in typeof(RotaRabbit).ObterConstantesPublicas<string>())
            {
                var args = ObterArgumentoDaFila(fila);
                channel.QueueDeclare(fila, true, false, false, args);
                channel.QueueBind(fila, ExchangeRabbit.SerapEstudanteAcompanhamento, fila, null);

                var argsDlq = ObterArgumentoDaFilaDeadLetter(fila);
                var filaDeadLetter = $"{fila}.deadletter";
                channel.QueueDeclare(filaDeadLetter, true, false, false, argsDlq);
                channel.QueueBind(filaDeadLetter, ExchangeRabbit.SerapEstudanteAcompanhamentoDeadLetter, fila, null);

                var argsFinal = new Dictionary<string, object> { { "x-queue-mode", "lazy" } };
                var filaDeadLetterFinal = $"{fila}.deadletter.final";
                channel.QueueDeclare(filaDeadLetterFinal, true, false, false, argsFinal);
                channel.QueueBind(filaDeadLetterFinal, ExchangeRabbit.SerapEstudanteAcompanhamentoDeadLetter, filaDeadLetterFinal, null);
            }
        }
        
        private Dictionary<string, object> ObterArgumentoDaFila(string fila)
        {
            var args = new Dictionary<string, object>
                { { "x-dead-letter-exchange", ExchangeRabbit.SerapEstudanteAcompanhamentoDeadLetter } };

            if (comandos.ContainsKey(fila) && comandos[fila].ModeLazy)
                args.Add("x-queue-mode", "lazy");
            
            return args;
        }
        
        private Dictionary<string, object> ObterArgumentoDaFilaDeadLetter(string fila)
        {
            var argsDlq = new Dictionary<string, object>();
            var ttl = comandos.ContainsKey(fila) ? comandos[fila].Ttl : ExchangeRabbit.SerapDeadLetterTtl;

            argsDlq.Add("x-dead-letter-exchange", ExchangeRabbit.Serap);
            argsDlq.Add("x-message-ttl", ttl);
            argsDlq.Add("x-queue-mode", "lazy");

            return argsDlq;
        }
        
        private ulong GetRetryCount(IBasicProperties properties)
        {
            if (properties.Headers == null || !properties.Headers.ContainsKey("x-death"))
                return 0;
            
            var deathProperties = (List<object>)properties.Headers["x-death"];
            var lastRetry = (Dictionary<string, object>)deathProperties[0];
            var count = lastRetry["count"];
            
            return (ulong) Convert.ToInt64(count);
        }        

        private void RegistrarUseCases()
        {
            comandos.Add(RotaRabbit.DeadLetterSync, new ComandoRabbit("Sincronização de dead letter", typeof(ITratarDeadletterSyncUseCase)));
            comandos.Add(RotaRabbit.DeadLetterTratar, new ComandoRabbit("Tratar dead letter", typeof(ITratarDeadletterUseCase)));

            comandos.Add(RotaRabbit.IniciarSync, new ComandoRabbit("Iniciar Sincronização", typeof(IIniciarSyncUseCase)));

            comandos.Add(RotaRabbit.DreSync, new ComandoRabbit("Sincronização institucional", typeof(ITratarDreSyncUseCase)));
            comandos.Add(RotaRabbit.DreTratar, new ComandoRabbit("Tratar dre", typeof(ITratarDreUseCase)));
            comandos.Add(RotaRabbit.UeSync, new ComandoRabbit("Sincronização de ue", typeof(ITratarUeSyncUseCase)));
            comandos.Add(RotaRabbit.UeTratar, new ComandoRabbit("Tratar ue", typeof(ITratarUeUseCase)));
            comandos.Add(RotaRabbit.TurmaSync, new ComandoRabbit("Sincronização de turmas", typeof(ITratarTurmaSyncUseCase)));
            comandos.Add(RotaRabbit.TurmaTratar, new ComandoRabbit("Tratar turma", typeof(ITratarTurmaUseCase)));

            comandos.Add(RotaRabbit.AnoSync, new ComandoRabbit("Sincronização de anos", typeof(ITratarAnoSyncUseCase)));
            comandos.Add(RotaRabbit.AnoTratar, new ComandoRabbit("Tratar anos", typeof(ITratarAnoUseCase)));

            comandos.Add(RotaRabbit.ProvaSync, new ComandoRabbit("Sincronização de provas", typeof(ITratarProvaSyncUseCase)));
            comandos.Add(RotaRabbit.ProvaTratar, new ComandoRabbit("Tratar provas", typeof(ITratarProvaUseCase)));

            comandos.Add(RotaRabbit.ProvaQuestaoSync, new ComandoRabbit("Sincronização de questões por prova", typeof(ITratarProvaQuestaoSyncUseCase)));
            comandos.Add(RotaRabbit.ProvaQuestaoTratar, new ComandoRabbit("Tratar questão prova", typeof(ITratarProvaQuestaoUseCase)));

            comandos.Add(RotaRabbit.AbrangenciaSync, new ComandoRabbit("Sincronização de abragencia", typeof(ITratarAbrangenciaSyncUseCase)));
            comandos.Add(RotaRabbit.AbrangenciaGrupoTratar, new ComandoRabbit("Tratar os usuários por grupo de abragencia", typeof(ITratarAbrangenciaGrupoUseCase)));
            comandos.Add(RotaRabbit.AbrangenciaGrupoUsuarioTratar, new ComandoRabbit("Tratar as abrangencias por usuário e grupo", typeof(ITratarAbrangenciaGrupoUsuarioUseCase)));

            comandos.Add(RotaRabbit.AbrangenciaExcluirTratar, new ComandoRabbit("Exclui Grupos removidos do coresso", typeof(ITratarAbrangenciaExcluirUseCase)));
            comandos.Add(RotaRabbit.AbrangenciaGrupoExcluirTratar, new ComandoRabbit("Exclui Usuários removidos dos grupos no coresso", typeof(ITratarAbrangenciaGrupoExcluirUseCase)));
            comandos.Add(RotaRabbit.AbrangenciaGrupoUsuarioExcluirTratar, new ComandoRabbit("Exclui abrangencia removida do usuário no coresso e eol", typeof(ITratarAbrangenciaGrupoUsuarioExcluirUseCase)));

            comandos.Add(RotaRabbit.AbrangenciaTratar, new ComandoRabbit("Tratar abrangencia", typeof(ITratarAbrangenciaUseCase)));
            comandos.Add(RotaRabbit.AbrangenciaExcluir, new ComandoRabbit("Excluir abrangencia", typeof(IExcluirAbrangenciaUseCase)));

            comandos.Add(RotaRabbit.ProvaAlunoSync, new ComandoRabbit("Sincronização prova aluno", typeof(ITratarProvaAlunoSyncUseCase)));
            comandos.Add(RotaRabbit.ProvaAlunoTurmaSync, new ComandoRabbit("Sincronização prova aluno turma", typeof(ITratarProvaAlunoTurmaSyncUseCase)));
            comandos.Add(RotaRabbit.ProvaAlunoTratar, new ComandoRabbit("tratar prova aluno", typeof(ITratarProvaAlunoUseCase)));

            comandos.Add(RotaRabbit.ProvaAlunoResultadoTratar, new ComandoRabbit("tratar prova aluno resultado", typeof(ITratarProvaAlunoResultadoUseCase)));
            comandos.Add(RotaRabbit.ProvaTurmaResultadoTratar, new ComandoRabbit("tratar prova turma resultado", typeof(ITratarProvaTurmaResultadoUseCase)));
            comandos.Add(RotaRabbit.ProvaTurmaResultadoRecalcular, new ComandoRabbit("tratar prova turma resultado recalcular", typeof(IRecalcularProvaTurmaResultadoUseCase)));

            comandos.Add(RotaRabbit.ProvaAlunoRespostaSync, new ComandoRabbit("Sincronização prova turma aluno resposta", typeof(ITratarProvaAlunoRespostaSyncUseCase)));
            comandos.Add(RotaRabbit.ProvaAlunoRespostaTratar, new ComandoRabbit("tratar prova turma aluno resposta", typeof(ITratarProvaAlunoRespostaUseCase)));
            comandos.Add(RotaRabbit.ProvaAlunoRespostaConsolidar, new ComandoRabbit("consolidar resposta prova aluno", typeof(IConsolidarProvaAlunoRespostaUseCase)));

            comandos.Add(RotaRabbit.ProvaAlunoDownloadTratar, new ComandoRabbit("tratar download prova aluno", typeof(ITratarProvaAlunoResultadoDownloadUseCase)));
            comandos.Add(RotaRabbit.ProvaAlunoInicioFimTratar, new ComandoRabbit("tratar inicio e fim prova aluno", typeof(ITratarProvaAlunoResultadoInicioFimUseCase)));
            comandos.Add(RotaRabbit.ProvaAlunoReaberturaTratar, new ComandoRabbit("tratar prova aluno reabertura ", typeof(ITratarProvaAlunoReaberturaUseCase)));
        }

        private async Task InicializaConsumer(IModel channel, CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (ch, ea) =>
            {
                try
                {
                    await TratarMensagem(ea, channel);
                }
                catch (Exception ex)
                {
                    servicoLog.Registrar($"Erro ao tratar mensagem {ea.DeliveryTag}", ex);
                    channel.BasicReject(ea.DeliveryTag, false);
                }
            };

            RegistrarConsumer(consumer, channel);

            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker ativo em: {Now}", DateTime.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }

        private async Task TratarMensagem(BasicDeliverEventArgs ea, IModel channel)
        {
            var mensagem = Encoding.UTF8.GetString(ea.Body.Span);
            var rota = ea.RoutingKey;

            if (comandos.ContainsKey(rota))
            {
                var transacao = servicoTelemetria.IniciarTransacao(rota);

                var mensagemRabbit = mensagem.ConverterObjectStringPraObjeto<MensagemRabbit>();
                var comandoRabbit = comandos[rota];

                try
                {
                    using var scope = serviceScopeFactory.CreateScope();
                    var casoDeUso = scope.ServiceProvider.GetService(comandoRabbit.TipoCasoUso);

                    if (casoDeUso == null) 
                        throw new ArgumentNullException(comandoRabbit.TipoCasoUso.Name);

                    await servicoTelemetria.RegistrarAsync(() =>
                        comandoRabbit.TipoCasoUso.ObterMetodo("Executar").InvokeAsync(casoDeUso, mensagemRabbit),
                        "RabbitMQ",
                        rota,
                        rota);

                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (NegocioException nex)
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                    RegistrarLog(ea, mensagemRabbit, nex, LogNivel.Negocio, $"Erros: {nex.Message}");
                    servicoTelemetria.RegistrarExcecao(transacao, nex);
                }
                catch (ValidacaoException vex)
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                    RegistrarLog(ea, mensagemRabbit, vex, LogNivel.Negocio, $"Erros: {JsonSerializer.Serialize(vex.Mensagens())}");
                    servicoTelemetria.RegistrarExcecao(transacao, vex);
                }
                catch (Exception ex)
                {
                    var rejeicao = GetRetryCount(ea.BasicProperties);

                    if (++rejeicao >= comandoRabbit.QuantidadeReprocessamentoDeadLetter)
                    {
                        channel.BasicReject(ea.DeliveryTag, false);
                        
                        var filaFinal = $"{ea.RoutingKey}.deadletter.final";
                        await servicoMensageria.Publicar(mensagemRabbit, filaFinal, ExchangeRabbit.SerapDeadLetter,
                            "PublicarDeadLetter");                        
                    } else
                        channel.BasicReject(ea.DeliveryTag, false);
                    
                    RegistrarLog(ea, mensagemRabbit, ex, LogNivel.Critico, $"Erros: {ex.Message}");
                    servicoTelemetria.RegistrarExcecao(transacao, ex);
                }
                finally
                {
                    servicoTelemetria.FinalizarTransacao(transacao);
                }
            }
            else
                channel.BasicReject(ea.DeliveryTag, false);
        }

        private static void RegistrarConsumer(EventingBasicConsumer consumer, IModel channel)
        {
            foreach (var fila in typeof(RotaRabbit).ObterConstantesPublicas<string>())
                channel.BasicConsume(fila, false, consumer);
        }

        private void RegistrarLog(BasicDeliverEventArgs ea, MensagemRabbit mensagemRabbit, Exception ex, LogNivel logNivel, string observacao)
        {
            var mensagem = $"Worker Serap: Rota -> {ea.RoutingKey}  Cod Correl -> {mensagemRabbit.CodigoCorrelacao.ToString()[..3]}";
            servicoLog.Registrar(new LogMensagem(mensagem, logNivel, observacao, ex?.StackTrace, ex.InnerException?.Message));
        }
    }
}