namespace SME.SERAp.Prova.Acompanhamento.Dominio.Entities
{
    public class Ano : EntidadeBase
    {
        public Ano(long id, int anoLetivo, long ueId, string nome)
        {
            Id = id.ToString();
            AnoLetivo = anoLetivo;
            UeId = ueId;
            Nome = nome;
        }

        public int AnoLetivo { get; set; }
        public long UeId { get; set; }
        public string Nome { get; set; }
    }
}
