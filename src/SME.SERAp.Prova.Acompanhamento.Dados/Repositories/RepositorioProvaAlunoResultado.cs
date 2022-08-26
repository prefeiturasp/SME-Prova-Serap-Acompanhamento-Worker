using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioProvaAlunoResultado : RepositorioBase<ProvaAlunoResultado>, IRepositorioProvaAlunoResultado
    {
        protected override string IndexName => "prova-aluno-resultado";
        public RepositorioProvaAlunoResultado(IElasticClient elasticClient) : base(elasticClient)
        {
        }

        public async Task<IEnumerable<ProvaAlunoResultado>> ObterPorProvaAlunoRaAsync(long provaId, long alunoRa)
        {
            var search = new SearchDescriptor<ProvaAlunoResultado>(IndexName).Query(q =>
                q.Term(t => t.Field(f => f.ProvaId).Value(provaId)) &&
                q.Term(t => t.Field(f => f.AlunoRa).Value(alunoRa))
            );

            var response = await elasticClient.SearchAsync<ProvaAlunoResultado>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }

        public async Task<ProvaAlunoResultado> ObterPorProvaAlunoIdAsync(long provaId, long alunoId)
        {
            var search = new SearchDescriptor<ProvaAlunoResultado>(IndexName).Query(q =>
                q.Term(t => t.Field(f => f.ProvaId).Value(provaId)) &&
                q.Term(t => t.Field(f => f.AlunoId).Value(alunoId))
            );

            var response = await elasticClient.SearchAsync<ProvaAlunoResultado>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).FirstOrDefault();
        }

        public async Task<IEnumerable<ProvaAlunoResultado>> ObterPorProvaTurmaIdAsync(long provaId, long turmaId)
        {
            var search = new SearchDescriptor<ProvaAlunoResultado>(IndexName).Query(q =>
                q.Term(t => t.Field(f => f.ProvaId).Value(provaId)) &&
                q.Term(t => t.Field(f => f.TurmaId).Value(turmaId))
            );

            var response = await elasticClient.SearchAsync<ProvaAlunoResultado>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }
    }
}
