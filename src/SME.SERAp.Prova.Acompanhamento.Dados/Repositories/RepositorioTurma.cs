using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioTurma : RepositorioBase<Turma>, IRepositorioTurma
    {
        public RepositorioTurma(ElasticOptions elasticOptions, IElasticClient elasticClient) : base(elasticOptions, elasticClient)
        {
        }

        public async Task<Turma> ObterPorCodigoAsync(int ano, long codigo)
        {
            var search = new SearchDescriptor<Turma>(IndexName)
                .Query(q =>
                    q.Term(t => t.Field(f => f.AnoLetivo).Value(ano)) &&
                    q.Term(t => t.Field(f => f.Codigo).Value(codigo))
                );

            var response = await elasticClient.SearchAsync<Turma>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).FirstOrDefault();
        }
    }
}
