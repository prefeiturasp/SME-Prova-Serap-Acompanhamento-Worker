

using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes
{
    public class ProvaQuestaoDto
    {

        public ProvaQuestaoDto()
        {

        }

        public ProvaQuestaoDto(long provaId, long questaoId, AcaoCrud acao)
        {
            ProvaId = provaId;
            QuestaoId = questaoId;
            Acao = acao;
        }

        public long ProvaId { get; set; }
        public long QuestaoId { get; set; }
        public AcaoCrud Acao { get; set; }
    }
}
