
namespace SME.SERAp.Prova.Acompanhamento.Dominio.Entities
{
    public class ProvaQuestao : EntidadeBase
    {
        public ProvaQuestao() { }
        public ProvaQuestao(long provaId, long questaoId)
        {
            ProvaId = provaId;
            QuestaoId = questaoId;
        }

        public long ProvaId { get; set; }
        public long QuestaoId { get; set; }
    }
}
