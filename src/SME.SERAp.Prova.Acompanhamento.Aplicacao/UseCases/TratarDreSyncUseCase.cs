using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarDreSyncUseCase : AbstractUseCase, ITratarDreSyncUseCase
    {
        public TratarDreSyncUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var dres = await mediator.Send(new ObterTodasDresQuerySerap());

            if (dres != null && dres.Any())
            {
                foreach (var dre in dres)
                {
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.DreTratar, dre));
                }
            }

            return true;
        }
    }
}
