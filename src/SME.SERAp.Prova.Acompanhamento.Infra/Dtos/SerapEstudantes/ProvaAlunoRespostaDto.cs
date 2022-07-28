namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes
{
    public class ProvaAlunoRespostaDto
    {
        public long ProvaId { get; set; }
        public long AlunoRa { get; set; }
        public long QuestaoId { get; set; }
        public long? AlternativaId { get; set; }
        public int? Tempo { get; set; }
        public bool Consolidar { get; set; } = true;
    }
}
