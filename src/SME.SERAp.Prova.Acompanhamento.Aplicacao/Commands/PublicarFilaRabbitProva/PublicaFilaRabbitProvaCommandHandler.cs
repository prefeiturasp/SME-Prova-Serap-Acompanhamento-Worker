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

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Commands.PublicarFilaRabbitProva
{
    public class PublicaFilaRabbitProvaCommandHandler : IRequestHandler<PublicaFilaRabbitProvaCommand, bool>
    {
        private readonly IServicoLog servicoLog;
        private readonly IModel model;
        public PublicaFilaRabbitProvaCommandHandler(IServicoLog servicoLog, IModel model)
        {
            this.servicoLog = servicoLog ?? throw new ArgumentNullException(nameof(servicoLog));
            this.model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public Task<bool> Handle(PublicaFilaRabbitProvaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mensagem = new MensagemRabbit(request.Mensagem, Guid.NewGuid());

                var mensagemJson = JsonSerializer.Serialize(mensagem);
                var body = Encoding.UTF8.GetBytes(mensagemJson);

                var props = model.CreateBasicProperties();
                props.Persistent = true;

                model.BasicPublish(ExchangeRabbit.SerapEstudanteProva, request.NomeRota, props, body);

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                servicoLog.Registrar(LogNivel.Critico, $"Erros: PublicaFilaRabbitProvaCommand --{ex.Message}", $"Worker Serap: Rota -> {request.NomeRota} Fila -> {request.NomeFila}", ex.StackTrace);
                return Task.FromResult(false);
            }
        }
    }
}
