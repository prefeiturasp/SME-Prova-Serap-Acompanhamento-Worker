
namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class ProvaTurmaRecalcularDto
    {
        public ProvaTurmaRecalcularDto(long provaId, long turmaId)
        {
            ProvaId = provaId;
            TurmaId = turmaId;
        }

        public long ProvaId { get; set; }
        public long TurmaId { get; set; }
    }
}
