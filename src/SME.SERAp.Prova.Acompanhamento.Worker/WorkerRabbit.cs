using Elastic.Apm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SME.SERAp.Prova.Acompanhamento.Aplicacao;
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

        private readonly Dictionary<string, ComandoRabbit> comandos;

        public WorkerRabbit(
            IServiceScopeFactory serviceScopeFactory,
            RabbitOptions rabbitOptions,
            ConnectionFactory connectionFactory,
            ILogger<WorkerRabbit> logger,
            IServicoTelemetria servicoTelemetria,
            IServicoLog servicoLog)
        {
            this.serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            this.rabbitOptions = rabbitOptions ?? throw new ArgumentNullException(nameof(rabbitOptions));
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.servicoTelemetria = servicoTelemetria ?? throw new ArgumentNullException(nameof(servicoTelemetria));
            this.servicoLog = servicoLog ?? throw new ArgumentNullException(nameof(servicoLog));
            this.comandos = new Dictionary<string, ComandoRabbit>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var conexaoRabbit = connectionFactory.CreateConnection();
            using IModel channel = conexaoRabbit.CreateModel();

            var props = channel.CreateBasicProperties();
            props.Persistent = true;

            channel.BasicQos(0, rabbitOptions.LimiteDeMensagensPorExecucao, false);

            channel.ExchangeDeclare(ExchangeRabbit.SerapEstudanteAcompanhamento, ExchangeType.Direct, true, false);
            channel.ExchangeDeclare(ExchangeRabbit.SerapEstudanteAcompanhamentoDeadLetter, ExchangeType.Direct, true, false);

            DeclararFilas(channel);

            RegistrarUseCases();

            await InicializaConsumer(channel, stoppingToken);
        }

        private static void DeclararFilas(IModel channel)
        {
            foreach (var fila in typeof(RotaRabbit).ObterConstantesPublicas<string>())
            {
                var args = new Dictionary<string, object>()
                    {
                        { "x-dead-letter-exchange", ExchangeRabbit.SerapEstudanteAcompanhamentoDeadLetter }
                    };

                channel.QueueDeclare(fila, true, false, false, args);
                channel.QueueBind(fila, ExchangeRabbit.SerapEstudanteAcompanhamento, fila, null);

                var filaDeadLetter = $"{fila}.deadletter";
                channel.QueueDeclare(filaDeadLetter, true, false, false, null);
                channel.QueueBind(filaDeadLetter, ExchangeRabbit.SerapEstudanteAcompanhamentoDeadLetter, fila, null);

                var filaDeadLetterFinal = $"{fila}.deadletter.final";
                channel.QueueDeclare(filaDeadLetterFinal, true, false, false, null);
                channel.QueueBind(filaDeadLetterFinal, ExchangeRabbit.SerapEstudanteAcompanhamentoDeadLetter, filaDeadLetterFinal, null);
            }
        }

        private void RegistrarUseCases()
        {
            comandos.Add(RotaRabbit.Teste, new ComandoRabbit("Teste", typeof(ITesteUseCase)));
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
                logger.LogInformation(
                    $"Worker ativo em: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                await Task.Delay(10000, stoppingToken);
            }
        }

        private async Task TratarMensagem(BasicDeliverEventArgs ea, IModel channel)
        {
            var mensagem = Encoding.UTF8.GetString(ea.Body.Span);
            var rota = ea.RoutingKey;
            if (comandos.ContainsKey(rota))
            {
                var mensagemRabbit = mensagem.ConverterObjectStringPraObjeto<MensagemRabbit>();
                var comandoRabbit = comandos[rota];

                var transacao = servicoTelemetria.Apm ?
                    Agent.Tracer.StartTransaction(rota, "WorkerRabbitSerapAcompanhamento") :
                    null;
                try
                {

                    using var scope = serviceScopeFactory.CreateScope();
                    var casoDeUso = scope.ServiceProvider.GetService(comandoRabbit.TipoCasoUso);

                    await servicoTelemetria.RegistrarAsync(() =>
                        comandoRabbit.TipoCasoUso.ObterMetodo("Executar").InvokeAsync(casoDeUso, new object[] { mensagemRabbit }),
                        "RabbitMQ",
                        rota,
                        rota);

                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (NegocioException nex)
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                    RegistrarLog(ea, mensagemRabbit, nex, LogNivel.Negocio, $"Erros: {nex.Message}");
                    transacao.CaptureException(nex);
                }
                catch (ValidacaoException vex)
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                    RegistrarLog(ea, mensagemRabbit, vex, LogNivel.Negocio, $"Erros: {JsonSerializer.Serialize(vex.Mensagens())}");
                    transacao.CaptureException(vex);
                }
                catch (Exception ex)
                {
                    channel.BasicReject(ea.DeliveryTag, false);
                    RegistrarLog(ea, mensagemRabbit, ex, LogNivel.Critico, $"Erros: {ex.Message}");
                    transacao.CaptureException(ex);
                }
                finally
                {
                    transacao?.End();
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