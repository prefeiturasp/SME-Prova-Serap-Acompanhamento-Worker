using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioUe : RepositorioBase<Ue>, IRepositorioUe
    {
        protected override string IndexName => "ue";
        public RepositorioUe(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}
