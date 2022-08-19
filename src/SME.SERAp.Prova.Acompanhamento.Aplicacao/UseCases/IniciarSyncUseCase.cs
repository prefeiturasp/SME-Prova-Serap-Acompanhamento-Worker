using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class IniciarSyncUseCase : AbstractUseCase, IIniciarSyncUseCase
    {
        public IniciarSyncUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.DreSync));
            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaSync));
            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.AnoSync));
            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.AbrangenciaSync));

            //await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaAlunoSync));
            return true;
        }
    }
}
