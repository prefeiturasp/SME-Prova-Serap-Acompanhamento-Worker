using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaAlunoUseCase : AbstractUseCase, ITratarProvaAlunoUseCase
    {
        public TratarProvaAlunoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provaTurma = mensagemRabbit.ObterObjetoMensagem<ProvaTurmaDto>();
            if (provaTurma == null) return false;

            var alunos = await mediator.Send(new ObterAlunosTurmaSerapQuery(provaTurma.TurmaId, provaTurma.Inicio, provaTurma.Fim));
            if (alunos != null && alunos.Any())
            {
                foreach (var aluno in alunos)
                {
                    var situacaoAlunoProva = await mediator.Send(new ObterSituacaoAlunoProvaSerapQuery(provaTurma.ProvaId, aluno.Ra));

                    var provaAlunoResultado = new ProvaAlunoResultado(
                        provaTurma.AnoLetivo,
                        provaTurma.ProvaId,
                        provaTurma.Inicio,
                        provaTurma.Fim,
                        provaTurma.DreId,
                        provaTurma.UeId,
                        provaTurma.Ano,
                        provaTurma.Modalidade,
                        provaTurma.TurmaId,
                        aluno.Id,
                        aluno.Ra,
                        aluno.Nome,
                        aluno.NomeSocial,
                        situacaoAlunoProva.FezDownload,
                        situacaoAlunoProva.Inicio,
                        situacaoAlunoProva.Fim,
                        situacaoAlunoProva.TempoMedio,
                        situacaoAlunoProva.QuestaoRespondida
                        );

                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaAlunoResultadoTratar, provaAlunoResultado));
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaAlunoRespostaSync, new { provaTurma.ProvaId, AlunoRa = aluno.Ra }));
                }
            }

            return true;
        }
    }
}
