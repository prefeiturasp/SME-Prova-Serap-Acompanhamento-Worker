using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class RemoverAlunoDuplicadoUseCase : AbstractUseCase, IRemoverAlunoDuplicadoUseCase
    {
        public RemoverAlunoDuplicadoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var turmaId = long.Parse(mensagemRabbit.Mensagem.ToString());

            var provasAlunosResultados = await mediator.Send(new ObterProvaAlunoResultadoPorTurmaIdQuery(turmaId));
            if (provasAlunosResultados == null)
                return false;

            var duplicados = provasAlunosResultados
                .GroupBy(c => new { c.ProvaId, c.AlunoRa })
                .Where(c => c.Count() > 1)
                .Select(c => c.Key);

            foreach (var duplicado in duplicados)
            {
                var provasAlunosResultadosRemover = provasAlunosResultados
                    .Where(c => c.ProvaId == duplicado.ProvaId
                                && c.AlunoRa == duplicado.AlunoRa);

                var primeiroRegistro = true;

                foreach (var provaAlunoResultadoRemover in provasAlunosResultadosRemover)
                {
                    if (primeiroRegistro)
                    {
                        primeiroRegistro = false;
                        continue;
                    }

                    provaAlunoResultadoRemover.InutilizarRegistro();
                    await mediator.Send(new AlterarProvaAlunoResultadoCommand(provaAlunoResultadoRemover));
                }
            }

            return true;
        }
    }
}