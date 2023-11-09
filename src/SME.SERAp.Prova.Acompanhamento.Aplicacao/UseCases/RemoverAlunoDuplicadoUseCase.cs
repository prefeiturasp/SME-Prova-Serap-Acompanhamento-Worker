using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class RemoverAlunoDuplicadoUseCase : AbstractUseCase, IRemoverAlunoDuplicadoUseCase
    {
        public RemoverAlunoDuplicadoUseCase(IMediator mediator) : base(mediator)
        {
        }        
        
        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var turmas = await mediator.Send(new ObterTurmasQuery());

            foreach (var turma in turmas)
            {
                var provasAlunosResultados = await mediator.Send(new ObterProvaAlunoResultadoPorTurmaIdQuery(long.Parse(turma.Id)));
                
                if (provasAlunosResultados == null)
                    continue;

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
            }

            return true;
        }
    }
}