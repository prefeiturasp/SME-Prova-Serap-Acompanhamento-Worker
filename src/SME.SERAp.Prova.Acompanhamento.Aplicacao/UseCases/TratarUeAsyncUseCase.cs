using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarUeSyncUseCase : AbstractUseCase, ITratarUeSyncUseCase
    {
        public TratarUeSyncUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var dreId = long.Parse(mensagemRabbit.Mensagem.ToString());
            var ues = await mediator.Send(new ObterTodasUesPorDreIdSerapQuery(dreId));
            if (ues != null && ues.Any())
            {
                foreach (var ue in ues)
                {
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.UeTratar, ue));
                }
            }

            return true;
        }
    }
}
