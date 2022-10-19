using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioUe : RepositorioBase<Ue>, IRepositorioUe
    {
        public RepositorioUe(ElasticOptions elasticOptions, IElasticClient elasticClient) : base(elasticOptions, elasticClient)
        {
        }

        public async Task<Ue> ObterPorCodigoAsync(long codigo)
        {
            var search = new SearchDescriptor<Ue>(IndexName)
                .Query(q => q.Term(t => t.Field(f => f.Codigo).Value(codigo)));

            var response = await elasticClient.SearchAsync<Ue>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).FirstOrDefault();
        }
    }
}
