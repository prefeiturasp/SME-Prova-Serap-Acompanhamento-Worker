using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ExcluirAbrangenciaUseCase : AbstractUseCase, IExcluirAbrangenciaUseCase
    {
        public ExcluirAbrangenciaUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var abrangenciaId = mensagemRabbit.Mensagem.ToString();

            if (string.IsNullOrEmpty(abrangenciaId)) return false;

            return await mediator.Send(new ExcluirAbrangenciaCommand(abrangenciaId));
        }
    }
}
