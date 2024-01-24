using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioProvaAlunoResultado : RepositorioBase<ProvaAlunoResultado>, IRepositorioProvaAlunoResultado
    {
        private const string ScrollTime = "1m";
        
        public RepositorioProvaAlunoResultado(ElasticOptions elasticOptions, IElasticClient elasticClient) : base(elasticOptions, elasticClient)
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
            ).Size(10000);

            var response = await elasticClient.SearchAsync<ProvaAlunoResultado>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).FirstOrDefault();
        }

        public async Task<IEnumerable<ProvaAlunoResultado>> ObterPorProvaTurmaIdAsync(long provaId, long turmaId)
        {
            var search = new SearchDescriptor<ProvaAlunoResultado>(IndexName).Query(q =>
                q.Term(t => t.Field(f => f.ProvaId).Value(provaId)) &&
                q.Term(t => t.Field(f => f.TurmaId).Value(turmaId))
            ).Size(10000);

            var response = await elasticClient.SearchAsync<ProvaAlunoResultado>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }

        public async Task<IEnumerable<ProvaAlunoResultado>> ObterPorTurmaIdAsync(long turmaId)
        {
            var search = new SearchDescriptor<ProvaAlunoResultado>(IndexName)
                .Query(q => q.Term(t =>
                    t.Field(f => f.TurmaId).Value(turmaId))).Scroll(ScrollTime);
            
            var response = await elasticClient.SearchAsync<ProvaAlunoResultado>(search);
            
            if (!response.IsValid || !response.Hits.Any())
                return default;

            var retorno = new List<ProvaAlunoResultado>();
            
            while (response.Hits.Any())
            {
                retorno.AddRange(response.Hits.Select(hit => hit.Source).ToList());
                response = await elasticClient.ScrollAsync<ProvaAlunoResultado>(ScrollTime, response.ScrollId);
            }

            return retorno;
        }

        public async Task<IEnumerable<ProvaAlunoResultado>> ObterPorProvaIdAsync(long provaId)
        {
            var search = new SearchDescriptor<ProvaAlunoResultado>(IndexName)
                .Query(q => q.Term(t =>
                    t.Field(f => f.ProvaId).Value(provaId))).Size(10000).Scroll(ScrollTime);
            
            var response = await elasticClient.SearchAsync<ProvaAlunoResultado>(search);
            
            if (!response.IsValid || !response.Hits.Any())
                return default;

            var retorno = new List<ProvaAlunoResultado>();
            
            while (response.Hits.Any())
            {
                retorno.AddRange(response.Hits.Select(hit => hit.Source).ToList());
                response = await elasticClient.ScrollAsync<ProvaAlunoResultado>(ScrollTime, response.ScrollId);
            }

            return retorno;
        }

        public async Task<bool> DeletarPorProvaIdAsync(long provaId)
        {
            var response = await elasticClient.DeleteByQueryAsync<ProvaAlunoResultado>(q => q
            .Query(q => q.Term(t => t.Field(f => f.ProvaId).Value(provaId))).Index(IndexName));

            if (!response.IsValid)
                throw new Exception(response.ServerError?.ToString(), response.OriginalException);

            return true;
        }

        public async Task<RetornoPaginadoDto<ProvaAlunoResultado>> ObterPaginadoAsync(long provaId, long turmaId, string scrollId)
        {
            var scrollTime = "1m";
            var search = new SearchDescriptor<ProvaAlunoResultado>(IndexName).Query(q =>
                q.Term(t => t.Field(f => f.ProvaId).Value(provaId)) &&
                q.Term(t => t.Field(f => f.TurmaId).Value(turmaId)))
                .Scroll(scrollTime);

            ISearchResponse<ProvaAlunoResultado> response;
            if (string.IsNullOrEmpty(scrollId))
                response = await elasticClient.SearchAsync<ProvaAlunoResultado>(search);
            else
                response = await elasticClient.ScrollAsync<ProvaAlunoResultado>(scrollTime, scrollId);

            if (response.Hits.Any())
                return new RetornoPaginadoDto<ProvaAlunoResultado> { ScrollId = response.ScrollId, Items = response.Hits.Select(hit => hit.Source) };

            return default;
        }
    }
}
