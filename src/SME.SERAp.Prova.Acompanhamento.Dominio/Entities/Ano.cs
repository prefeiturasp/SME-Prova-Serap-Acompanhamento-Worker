using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;

namespace SME.SERAp.Prova.Acompanhamento.Dominio.Entities
{
    public class Ano : EntidadeBase
    {
        public Ano() { }
        public Ano(long id, int anoLetivo, Modalidade modalidade, long ueId, string nome)
        {
            Id = id.ToString();
            AnoLetivo = anoLetivo;
            Modalidade = modalidade;
            UeId = ueId;
            Nome = nome;
        }

        public int AnoLetivo { get; set; }
        public Modalidade Modalidade { get; set; }
        public long UeId { get; set; }
        public string Nome { get; set; }
    }
}
