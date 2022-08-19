using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaAlunoSyncUseCase : AbstractUseCase, ITratarProvaAlunoSyncUseCase
    {
        public TratarProvaAlunoSyncUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provas = await mediator.Send(new ObterProvasEmAndamentoQuery());
            if (provas != null && provas.Any())
            {
                foreach (var prova in provas)
                {
                    var provasTurmas = await mediator.Send(new ObterProvasTurmasSerapQuery(prova.Id));
                    if (provasTurmas != null && provasTurmas.Any())
                    {
                        foreach (var provaTurma in provasTurmas)
                        {
                            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaAlunoTratar, provaTurma));
                        }
                    }
                }
            }

            return true;
        }
    }
}
