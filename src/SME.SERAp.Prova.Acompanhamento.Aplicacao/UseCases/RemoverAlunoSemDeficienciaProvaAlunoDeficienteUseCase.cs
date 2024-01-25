using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class RemoverAlunoSemDeficienciaProvaAlunoDeficienteUseCase : AbstractUseCase, IRemoverAlunoSemDeficienciaProvaAlunoDeficienteUseCase
    {
        public RemoverAlunoSemDeficienciaProvaAlunoDeficienteUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var mensagem = mensagemRabbit.Mensagem.ToString();
            if (mensagem == null)
                return false;
            
            var provaId = long.Parse(mensagem);
            if (provaId <= 0)
                return false;

            var prova = await mediator.Send(new ObterProvaParaDeficientePorProvaIdQuery(provaId));
            if (prova == null)
                return false;
            
            var provasAlunosResultados = await mediator.Send(new ObterProvaAlunoResultadoPorProvaIdQuery(provaId));
            if (provasAlunosResultados == null || !provasAlunosResultados.Any())
                return false;
            
            var deficiencias = await mediator.Send(new ObterDeficienciasPorProvaIdQuery(provaId));
            if (deficiencias == null || !deficiencias.Any())
                return false;
            
            var turmasIds = provasAlunosResultados.Select(c => c.TurmaId).Distinct();
            foreach (var turmaId in turmasIds)
            {
                var alunosComDeficiencias = await mediator.Send(new ObterAlunosTurmaSerapQuery(provaId, turmaId, true, deficiencias.ToArray()));
                var totalAlunos = 0;

                foreach (var provaAlunoResultadoTurma in provasAlunosResultados.Where(c => c.TurmaId == turmaId))
                {
                    if (provaAlunoResultadoTurma.AlunoQuestaoRespondida != null)
                    {
                        totalAlunos++;
                        continue;
                    }

                    if (alunosComDeficiencias.Select(c => c.Ra).Contains(provaAlunoResultadoTurma.AlunoRa))
                    {
                        totalAlunos++;
                        continue;
                    }

                    provaAlunoResultadoTurma.InutilizarRegistro();
                    await mediator.Send(new AlterarProvaAlunoResultadoCommand(provaAlunoResultadoTurma));                    
                }

                var provaTurmaResultado = await mediator.Send(new ObterProvaTurmaResultadoQuery(provaId, turmaId));
                if (provaTurmaResultado == null) 
                    continue;

                provaTurmaResultado.TotalAlunos = totalAlunos;
                await mediator.Send(new AlterarProvaTurmaResultadoCommand(provaTurmaResultado));
            }

            return true;
        }
    }
}