using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class RemoverDuplicadoSyncUseCase : AbstractUseCase, IRemoverDuplicadoSyncUseCase
    {
        public RemoverDuplicadoSyncUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var turmas = await mediator.Send(new ObterTurmasQuery());

            foreach (var turma in turmas)
            {
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.RemoverAlunoDuplicado, turma.Id));
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.RemoverRespostaDuplicado, turma.Id));
            }

            return true;
        }
    }
}
