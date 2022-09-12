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
    public class RepositorioAbrangencia : RepositorioBase<Abrangencia>, IRepositorioAbrangencia
    {
        public RepositorioAbrangencia(ElasticOptions elasticOptions, IElasticClient elasticClient) : base(elasticOptions, elasticClient)
        {
        }

        public async Task<IEnumerable<string>> ObterIdsPorGrupoUsuarioDiretenteAbrangenciaIds(Guid grupoId, Guid usuarioId, IEnumerable<string> abrangenciaIds)
        {
            var resultado = new List<string>();

            var query =
                new QueryContainerDescriptor<Abrangencia>().Term(t => t.Field(f => f.GrupoId).Value(grupoId)) &&
                new QueryContainerDescriptor<Abrangencia>().Term(t => t.Field(f => f.UsuarioId).Value(usuarioId));

            foreach (var abrangenciaId in abrangenciaIds)
                query &= !new QueryContainerDescriptor<Abrangencia>().Match(t => t.Field(f => f.Id).Query(abrangenciaId));

            var search = new SearchDescriptor<Abrangencia>(IndexName)
                .Query(_ => query)
                .Size(10000)
                .Scroll("10s");

            var response = await elasticClient.SearchAsync<Abrangencia>(search);

            while (response.Hits.Any())
            {
                resultado.AddRange(response.Hits.Select(hit => hit.Source.Id).ToList());

                response = await elasticClient.ScrollAsync<Abrangencia>("10s", response.ScrollId);
            }

            return resultado;
        }

        public async Task<IEnumerable<string>> ObterIdsDiretenteGrupoIds(Guid[] grupoIds)
        {
            var resultado = new List<string>();

            var query = new QueryContainer();

            foreach (var grupoId in grupoIds)
                query = query && !new QueryContainerDescriptor<Abrangencia>().Term(t => t.Field(f => f.GrupoId).Value(grupoId));

            var search = new SearchDescriptor<Abrangencia>(IndexName)
                .Query(_ => query)
                .Size(10000)
                .Scroll("10s");

            var response = await elasticClient.SearchAsync<Abrangencia>(search);

            while (response.Hits.Any())
            {
                resultado.AddRange(response.Hits.Select(hit => hit.Source.Id).ToList());

                response = await elasticClient.ScrollAsync<Abrangencia>("10s", response.ScrollId);
            }

            return resultado;
        }

        public async Task<IEnumerable<string>> ObterIdsPorGrupoDiretenteUsuarioIds(Guid grupoId, Guid[] usuarioIds)
        {
            var resultado = new List<string>();

            var query = new QueryContainerDescriptor<Abrangencia>().Term(t => t.Field(f => f.GrupoId).Value(grupoId));

            foreach (var usuarioId in usuarioIds)
                query = query && !new QueryContainerDescriptor<Abrangencia>().Term(t => t.Field(f => f.UsuarioId).Value(usuarioId));

            var search = new SearchDescriptor<Abrangencia>(IndexName)
                .Query(_ => query)
                .Size(10000)
                .Scroll("10s");

            var response = await elasticClient.SearchAsync<Abrangencia>(search);

            while (response.Hits.Any())
            {
                resultado.AddRange(response.Hits.Select(hit => hit.Source.Id).ToList());

                response = await elasticClient.ScrollAsync<Abrangencia>("10s", response.ScrollId);
            }

            return resultado;
        }
    }
}
