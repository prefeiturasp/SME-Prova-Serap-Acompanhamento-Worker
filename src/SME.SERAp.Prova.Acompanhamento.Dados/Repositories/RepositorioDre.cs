using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioDre : RepositorioBase<Dre>, IRepositorioDre
    {
        public RepositorioDre(ElasticOptions elasticOptions, IElasticClient elasticClient) : base(elasticOptions, elasticClient)
        {
        }

        public async Task<Dre> ObterPorCodigoAsync(long codigo)
        {
            var search = new SearchDescriptor<Dre>(IndexName)
                .Query(q => q.Term(t => t.Field(f => f.Codigo).Value(codigo)));

            var response = await elasticClient.SearchAsync<Dre>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).FirstOrDefault();
        }
    }
}
