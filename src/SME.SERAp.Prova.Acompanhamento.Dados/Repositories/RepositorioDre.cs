using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioDre : RepositorioBase<Dre>, IRepositorioDre
    {
        public RepositorioDre(ElasticOptions elasticOptions, IElasticClient elasticClient) : base(elasticOptions, elasticClient)
        {
        }
    }
}
