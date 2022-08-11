using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioProvaQuestao : RepositorioBase<ProvaQuestao>, IRepositorioProvaQuestao
    {
        protected override string IndexName => "prova-questao";
        public RepositorioProvaQuestao(IElasticClient elasticClient) : base(elasticClient)
        {

        }        

        public async Task<IEnumerable<ProvaQuestao>> ObterPorProvaIdAsync(long provaId)
        {
            var search = new SearchDescriptor<ProvaQuestao>(IndexName).Query(q =>
                          q.Term(t => t.Field(f => f.ProvaId).Value(provaId)));

            var response = await elasticClient.SearchAsync<ProvaQuestao>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }

        public async Task<ProvaQuestao> ObterPorQuestaoIdAsync(long questaoId)
        {
            var search = new SearchDescriptor<ProvaQuestao>(IndexName).Query(q =>
                          q.Term(t => t.Field(f => f.QuestaoId).Value(questaoId)));

            var response = await elasticClient.SearchAsync<ProvaQuestao>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).FirstOrDefault();
        }

        public async Task<bool> ExcluirAsync(ProvaQuestao provaQuestao)
        {
            var response = elasticClient.DeleteByQuery<ProvaQuestao>(q => q
            .Query(rq => rq
                .Match(m => m
                .Field(f => f.ProvaId).Query(provaQuestao.ProvaId.ToString())
                )
            ).Index(IndexName));

            return true;
        }
    }
}
