using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioDre : RepositorioBase<Dre>, IRepositorioDre
    {
        protected override string IndexName => "dre";
        public RepositorioDre(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}
