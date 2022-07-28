using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaSyncUseCase : AbstractUseCase, ITratarProvaSyncUseCase
    {
        public TratarProvaSyncUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provas = await mediator.Send(new ObterTodasProvasSerapQuery());

            if (provas != null && provas.Any())
            {
                foreach (var prova in provas)
                {
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaTratar, prova));
                }
            }

            return true;
        }
    }
}
