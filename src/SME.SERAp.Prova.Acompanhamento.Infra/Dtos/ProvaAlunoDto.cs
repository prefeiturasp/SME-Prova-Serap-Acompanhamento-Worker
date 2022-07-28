using System;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class ProvaAlunoDto
    {
        public long ProvaId { get; set; }
        public long AlunoRa { get; set; }
        public int Status { get; set; }
        public DateTime? CriadoEm { get; set; }
        public DateTime? FinalizadoEm { get; set; }
    }
}
