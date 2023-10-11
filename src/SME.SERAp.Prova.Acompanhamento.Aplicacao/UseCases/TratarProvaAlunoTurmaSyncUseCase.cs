using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaAlunoTurmaSyncUseCase : AbstractUseCase, ITratarProvaAlunoTurmaSyncUseCase
    {
        public TratarProvaAlunoTurmaSyncUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            long provaId = long.Parse(mensagemRabbit.Mensagem.ToString());

            var provasTurmas = await mediator.Send(new ObterProvasTurmasSerapQuery(provaId));
            if (provasTurmas != null && provasTurmas.Any())
            {
                foreach (var provaTurma in provasTurmas)
                {
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaAlunoTratar, provaTurma));
                }
            }

            return true;
        }
    }
}
