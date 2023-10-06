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
    public class RepositorioProvaAlunoResposta : RepositorioBase<ProvaAlunoResposta>, IRepositorioProvaAlunoResposta
    {
        public RepositorioProvaAlunoResposta(ElasticOptions elasticOptions, IElasticClient elasticClient) : base(elasticOptions, elasticClient)
        {
        }

        public async Task<bool> DeletarPorProvaIdAsync(long provaId)
        {

            var response = await elasticClient.DeleteByQueryAsync<ProvaAlunoResposta>(q => q
            .Query(q => q.Term(t => t.Field(f => f.ProvaId).Value(provaId))).Index(IndexName));

            if (!response.IsValid)
                throw new Exception(response.ServerError?.ToString(), response.OriginalException);

            return true;
        }

        public async Task<ProvaAlunoResposta> ObterPorProvaAlunoQuestaoAsync(long provaId, long alunoRa, long questaoId)
        {
            var search = new SearchDescriptor<ProvaAlunoResposta>(IndexName).Query(q =>
                           q.Term(t => t.Field(f => f.ProvaId).Value(provaId)) &&
                           q.Term(t => t.Field(f => f.AlunoRa).Value(alunoRa)) &&
                           q.Term(t => t.Field(f => f.QuestaoId).Value(questaoId))
                       ).Size(10000);

            var response = await elasticClient.SearchAsync<ProvaAlunoResposta>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).FirstOrDefault();
        }

        public async Task<IEnumerable<ProvaAlunoResposta>> ObterProvaAlunoAsync(long provaId, long alunoRa)
        {
            var search = new SearchDescriptor<ProvaAlunoResposta>(IndexName).Query(q =>
                          q.Term(t => t.Field(f => f.ProvaId).Value(provaId)) &&
                          q.Term(t => t.Field(f => f.AlunoRa).Value(alunoRa))
                      ).Size(10000);

            var response = await elasticClient.SearchAsync<ProvaAlunoResposta>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }
    }
}
