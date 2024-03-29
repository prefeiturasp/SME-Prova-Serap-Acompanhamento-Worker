﻿using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioProvaTurmaResultado : RepositorioBase<ProvaTurmaResultado>, IRepositorioProvaTurmaResultado
    {
        public RepositorioProvaTurmaResultado(ElasticOptions elasticOptions, IElasticClient elasticClient) : base(elasticOptions, elasticClient)
        {
        }

        public async Task<ProvaTurmaResultado> ObterPorProvaTurmaAsync(long provaId, long turmaId)
        {
            var search = new SearchDescriptor<ProvaTurmaResultado>(IndexName).Query(q =>
                q.Term(t => t.Field(f => f.ProvaId).Value(provaId)) &&
                q.Term(t => t.Field(f => f.TurmaId).Value(turmaId))
            ).Size(10000);

            var response = await elasticClient.SearchAsync<ProvaTurmaResultado>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).FirstOrDefault();
        }

        public async Task<bool> ExcluirPorProvaTurmaIdAsync(ProvaTurmaResultado provaTurmaResultado)
        {
            var response = await elasticClient.DeleteByQueryAsync<ProvaTurmaResultado>(q => q
            .Query(q => q.Term(t => t.Field(f => f.ProvaId).Value(provaTurmaResultado.ProvaId)) &&
                        q.Term(t => t.Field(f => f.TurmaId).Value(provaTurmaResultado.TurmaId))
                  ).Index(IndexName));

            if (!response.IsValid)
                throw new Exception(response.ServerError?.ToString(), response.OriginalException);

            return true;
        }

        public async Task<bool> ExcluirPorProvaIdAsync(long provaId)
        {
            var response = await elasticClient.DeleteByQueryAsync<ProvaTurmaResultado>(q => q
            .Query(q => q.Term(t => t.Field(f => f.ProvaId).Value(provaId))).Index(IndexName));

            if (!response.IsValid)
                throw new Exception(response.ServerError?.ToString(), response.OriginalException);

            return true;
        }

        public async Task<RetornoPaginadoDto<ProvaTurmaResultado>> ObterPaginadoAsync(long provaId, string scrollId)
        {
            var scrollTime = "1m";
            var search = new SearchDescriptor<ProvaTurmaResultado>(IndexName).Query(q =>
                q.Term(t => t.Field(f => f.ProvaId).Value(provaId)))
                .Scroll(scrollTime);

            ISearchResponse<ProvaTurmaResultado> response;
            if (string.IsNullOrEmpty(scrollId))
                response = await elasticClient.SearchAsync<ProvaTurmaResultado>(search);
            else
                response = await elasticClient.ScrollAsync<ProvaTurmaResultado>(scrollTime, scrollId);

            if (response.Hits.Any())
                return new RetornoPaginadoDto<ProvaTurmaResultado> { ScrollId = response.ScrollId, Items = response.Hits.Select(hit => hit.Source) };

            return default;
        }
    }
}
