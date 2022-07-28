using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes
{
    public class TurmaDto
    {
        public long Id { get; set; }
        public long UeId { get; set; }
        public long Codigo { get; set; }
        public int AnoLetivo { get; set; }
        public string Ano { get; set; }
        public string Nome { get; set; }
        public Modalidade Modalidade { get; set; }
        public int EtapaEja { get; set; }
        public Turno Turno { get; set; }
        public string SerieEnsino { get; set; }
    }
}
