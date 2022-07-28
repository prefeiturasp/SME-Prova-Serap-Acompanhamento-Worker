using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarAnoSyncUseCase : AbstractUseCase, ITratarAnoSyncUseCase
    {
        public TratarAnoSyncUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var anos = await mediator.Send(new ObterTodosAnosSerapQuery());

            if (anos != null && anos.Any())
            {
                foreach (var ano in anos)
                {
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.AnoTratar, ano));
                }
            }

            return true;
        }
    }
}
