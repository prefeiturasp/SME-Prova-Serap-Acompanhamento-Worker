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
            
            var provasTurmas = await mediator.Send(new ObterProvasTurmasSerapQuery(provaId));
            provasTurmas = provasTurmas.Where(c => c.TurmaId == 44496);
            if (provasTurmas == null || !provasTurmas.Any())
                return false;
            
            var provasTurmasParaDeficientes = provasTurmas.Where(c => c.Deficiente);
            if (!provasTurmasParaDeficientes.Any())
                return false;
            
            var deficiencias = await mediator.Send(new ObterDeficienciasPorProvaIdQuery(provaId));
            if (deficiencias == null || !deficiencias.Any())
                return false;
            
            var turmasIds = provasTurmasParaDeficientes.Select(c => c.TurmaId).Distinct();
            foreach (var turmaId in turmasIds)
            {
                var provasAlunosResultados = await mediator.Send(new ObterProvaAlunoResultadoPorProvaTurmaQuery(provaId, turmaId));
                if (provasAlunosResultados == null || !provasAlunosResultados.Any())
                    continue;

                var alunosComDeficiencias = await mediator.Send(new ObterAlunosTurmaSerapQuery(provaId, turmaId, true, deficiencias.ToArray()));

                foreach (var provaAlunoResultado in provasAlunosResultados)
                {
                    if (provaAlunoResultado.AlunoQuestaoRespondida != null)
                        continue;
                    
                    if (alunosComDeficiencias.Select(c => c.Ra).Contains(provaAlunoResultado.AlunoRa))
                        continue;
                    
                    provaAlunoResultado.InutilizarRegistro();
                    await mediator.Send(new AlterarProvaAlunoResultadoCommand(provaAlunoResultado));
                }
            }

            return true;
        }
    }
}