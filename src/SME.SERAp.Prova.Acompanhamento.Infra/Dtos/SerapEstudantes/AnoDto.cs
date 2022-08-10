using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes
{
    public class AnoDto
    {
        public long Id { get; set; }
        public int AnoLetivo { get; set; }
        public Modalidade Modalidade { get; set; }
        public long UeId { get; set; }
        public string Nome { get; set; }
    }
}
