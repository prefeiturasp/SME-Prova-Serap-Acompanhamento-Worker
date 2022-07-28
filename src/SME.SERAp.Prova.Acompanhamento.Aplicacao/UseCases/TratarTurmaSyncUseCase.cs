using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarTurmaSyncUseCase : AbstractUseCase, ITratarTurmaSyncUseCase
    {
        public TratarTurmaSyncUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var ueId = long.Parse(mensagemRabbit.Mensagem.ToString());

            var turmas = await mediator.Send(new ObterTodasTurmasPorUeIdSerapQuery(ueId));

            if (turmas != null && turmas.Any())
            {
                foreach (var turma in turmas)
                {
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.TurmaTratar, turma));
                }
            }

            return true;
        }
    }
}
