using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class RemoverRespostaDuplicadoUseCase : AbstractUseCase, IRemoverRespostaDuplicadoUseCase
    {
        public RemoverRespostaDuplicadoUseCase(IMediator mediator) : base(mediator)
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

                provasAlunosResultados = provasAlunosResultados.Where(t => t.SituacaoProvaAluno == Dominio.Enums.SituacaoProvaAluno.Finalizada);
                if (!provasAlunosResultados.Any())
                    continue;

                foreach (var provaAlunoResultado in provasAlunosResultados)
                {
                    var respostas = await mediator.Send(new ObterRespostasProvaAlunoQuery(provaAlunoResultado.ProvaId, provaAlunoResultado.AlunoRa));

                    var duplicados = respostas
                        .GroupBy(c => new { c.ProvaId, c.AlunoRa, c.QuestaoId })
                        .Where(c => c.Count() > 1)
                        .Select(c => c.Key);

                    foreach (var duplicado in duplicados)
                    {
                        var respostasRemover = respostas
                            .Where(c => c.ProvaId == duplicado.ProvaId
                                        && c.AlunoRa == duplicado.AlunoRa 
                                        && c.QuestaoId == duplicado.QuestaoId);

                        var primeiroRegistro = true;
                        foreach (var provaAlunoResposta in respostasRemover)
                        {
                            if (primeiroRegistro)
                            {
                                primeiroRegistro = false;
                                continue;
                            }

                            var dtoVazio = new ProvaAlunoRespostaDto();
                            await mediator.Send(new AlterarProvaAlunoQuestaoRespostaCommand(provaAlunoResposta.Id, dtoVazio));
                        }
                    }
                }
            }

            return true;
        }
    }
}