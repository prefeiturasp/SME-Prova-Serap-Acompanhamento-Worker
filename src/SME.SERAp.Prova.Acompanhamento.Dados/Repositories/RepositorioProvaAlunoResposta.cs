using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioProvaAlunoResposta : RepositorioBase<ProvaAlunoResposta>, IRepositorioProvaAlunoResposta
    {
        protected override string IndexName => "prova-aluno-resposta";
        public RepositorioProvaAlunoResposta(IElasticClient elasticClient) : base(elasticClient)
        {
        }

        public async Task<ProvaAlunoResposta> ObterPorProvaAlunoQuestaoAsync(long provaId, long alunoRa, long questaoId)
        {
            var search = new SearchDescriptor<ProvaAlunoResposta>(IndexName).Query(q =>
                           q.Term(t => t.Field(f => f.ProvaId).Value(provaId)) &&
                           q.Term(t => t.Field(f => f.AlunoRa).Value(alunoRa)) &&
                           q.Term(t => t.Field(f => f.QuestaoId).Value(questaoId))
                       );

            var response = await elasticClient.SearchAsync<ProvaAlunoResposta>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).FirstOrDefault();
        }

        public async Task<IEnumerable<ProvaAlunoResposta>> ObterProvaAlunoAsync(long provaId, long alunoRa)
        {
            var search = new SearchDescriptor<ProvaAlunoResposta>(IndexName).Query(q =>
                          q.Term(t => t.Field(f => f.ProvaId).Value(provaId)) &&
                          q.Term(t => t.Field(f => f.AlunoRa).Value(alunoRa))
                      );

            var response = await elasticClient.SearchAsync<ProvaAlunoResposta>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }
    }
}
