using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioProva : RepositorioBase<Dominio.Entities.Prova>, IRepositorioProva
    {
        protected override string IndexName => "prova";
        public RepositorioProva(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}
