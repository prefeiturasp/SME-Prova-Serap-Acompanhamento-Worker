using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes
{
    public class ProvaTurmaDto
    {
        public int AnoLetivo { get; set; }
        public long ProvaId { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public long DreId { get; set; }
        public long UeId { get; set; }
        public string Ano { get; set; }
        public Modalidade Modalidade { get; set; }
        public long TurmaId { get; set; }
    }
}
