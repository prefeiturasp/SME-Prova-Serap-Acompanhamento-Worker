using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioProvaQuestao : RepositorioBase<ProvaQuestao>, IRepositorioProvaQuestao
    {
        public RepositorioProvaQuestao(ElasticOptions elasticOptions, IElasticClient elasticClient) : base(elasticOptions, elasticClient)
        {
        }

        public async Task<IEnumerable<ProvaQuestao>> ObterPorProvaIdAsync(long provaId)
        {
            var resultado = new List<ProvaQuestao>();
            var search = new SearchDescriptor<ProvaQuestao>(IndexName)
                .Query(q => q.Term(t => t.Field(f => f.ProvaId).Value(provaId)))
                .Size(10000)
                .Scroll("10s");

            var response = await elasticClient.SearchAsync<ProvaQuestao>(search);
            while (response.Hits.Any())
            {
                resultado.AddRange(response.Hits.Select(hit => hit.Source));
                response = await elasticClient.ScrollAsync<ProvaQuestao>("10s", response.ScrollId);
            }

            return resultado;
        }

        public async Task<ProvaQuestao> ObterPorQuestaoIdAsync(long questaoId)
        {
            var search = new SearchDescriptor<ProvaQuestao>(IndexName).Query(q =>
                          q.Term(t => t.Field(f => f.QuestaoId).Value(questaoId))).Size(10000);

            var response = await elasticClient.SearchAsync<ProvaQuestao>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).FirstOrDefault();
        }

        public async Task<bool> ExcluirAsync(ProvaQuestao provaQuestao)
        {
            var response = await elasticClient.DeleteByQueryAsync<ProvaQuestao>(q => q
            .Query(q => q.Term(t => t.Field(f => f.ProvaId).Value(provaQuestao.ProvaId)) &&
                        q.Term(t => t.Field(f => f.QuestaoId).Value(provaQuestao.QuestaoId))
                  ).Index(IndexName));

            if (!response.IsValid)
                throw new Exception(response.ServerError?.ToString(), response.OriginalException);

            return true;
        }

        public async Task<bool> ExcluirPorProvaIdAsync(long provaId)
        {
            var response = await elasticClient.DeleteByQueryAsync<ProvaQuestao>(q => q
            .Query(q => q.Term(t => t.Field(f => f.ProvaId).Value(provaId))).Index(IndexName));

            if (!response.IsValid)
                throw new Exception(response.ServerError?.ToString(), response.OriginalException);

            return true;
        }
    }
}
