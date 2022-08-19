using System;

namespace SME.SERAp.Prova.Acompanhamento.Dominio.Entities
{
    public class ProvaAlunoResposta : EntidadeBase
    {
        public ProvaAlunoResposta() { }
        public ProvaAlunoResposta(long provaId, long alunoRa, long questaoId, long? alternativaId, int? tempo)
        {
            ProvaId = provaId;
            AlunoRa = alunoRa;
            QuestaoId = questaoId;
            AlternativaId = alternativaId;
            Tempo = tempo;

            Id = Guid.NewGuid().ToString();
        }

        public long ProvaId { get; set; }
        public long AlunoRa { get; set; }
        public long QuestaoId { get; set; }
        public long? AlternativaId { get; set; }
        public int? Tempo { get; set; }
    }
}
