using MediatR;
using RabbitMQ.Client;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using SME.SERAp.Prova.Acompanhamento.Infra.Interfaces;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static SME.SERAp.Prova.Acompanhamento.Infra.Services.ServicoLog;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class PublicaFilaRabbitCommandHandler : IRequestHandler<PublicaFilaRabbitCommand, bool>
    {
        private readonly IServicoLog servicoLog;
        private readonly ConnectionFactory connectionFactory;

        public PublicaFilaRabbitCommandHandler(IServicoLog servicoLog, ConnectionFactory connectionFactory)
        {
            this.servicoLog = servicoLog ?? throw new ArgumentNullException(nameof(servicoLog));
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public Task<bool> Handle(PublicaFilaRabbitCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mensagem = new MensagemRabbit(request.Mensagem, Guid.NewGuid());

                var mensagemJson = JsonSerializer.Serialize(mensagem);
                var body = Encoding.UTF8.GetBytes(mensagemJson);

                using var conexaoRabbit = connectionFactory.CreateConnection();
                using IModel channel = conexaoRabbit.CreateModel();

                var props = channel.CreateBasicProperties();
                props.Persistent = true;

                channel.BasicPublish(ExchangeRabbit.SerapEstudanteAcompanhamento, request.NomeRota, props, body);

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                servicoLog.Registrar(LogNivel.Critico, $"Erros: PublicaFilaRabbitCommand --{ex.Message}", $"Worker Serap: Rota -> {request.NomeRota} Fila -> {request.NomeFila}", ex.StackTrace);
                return Task.FromResult(false);
            }
        }
    }
}
