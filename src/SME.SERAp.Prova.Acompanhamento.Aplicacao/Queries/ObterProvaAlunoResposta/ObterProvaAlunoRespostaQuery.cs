using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaAlunoRespostaQuery : IRequest<ProvaAlunoResposta>
    {
        public ObterProvaAlunoRespostaQuery(long provaId, long alunoRa, long questaoId)
        {
            ProvaId = provaId;
            AlunoRa = alunoRa;
            QuestaoId = questaoId;
        }

        public long ProvaId { get; set; }
        public long AlunoRa { get; set; }
        public long QuestaoId { get; set; }
    }
}
