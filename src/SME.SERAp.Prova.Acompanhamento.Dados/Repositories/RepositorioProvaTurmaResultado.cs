using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioProvaTurmaResultado : RepositorioBase<ProvaTurmaResultado>, IRepositorioProvaTurmaResultado
    {
        protected override string IndexName => "prova-turma-resultado";
        public RepositorioProvaTurmaResultado(IElasticClient elasticClient) : base(elasticClient)
        {
        }

        public async Task<ProvaTurmaResultado> ObterPorProvaTurmaAsync(long provaId, long turmaId)
        {
            var search = new SearchDescriptor<ProvaAlunoResultado>(IndexName).Query(q =>
                q.Term(t => t.Field(f => f.ProvaId).Value(provaId)) &&
                q.Term(t => t.Field(f => f.TurmaId).Value(turmaId))
            );

            var response = await elasticClient.SearchAsync<ProvaTurmaResultado>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).FirstOrDefault();
        }
    }
}
