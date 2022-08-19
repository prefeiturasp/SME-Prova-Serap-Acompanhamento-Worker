namespace SME.SERAp.Prova.Acompanhamento.Dominio.Entities
{
    public class Dre : EntidadeBase
    {
        public Dre() { }
        public Dre(long id, long codigo, string abreviacao, string nome)
        {
            Id = id.ToString();
            Codigo = codigo;
            Abreviacao = abreviacao;
            Nome = nome;
        }

        public long Codigo { get; set; }
        public string Abreviacao { get; set; }
        public string Nome { get; set; }
    }
}
