using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
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

        public async Task<IEnumerable<string>> ObterIdsPorGrupoUsuarioDiretenteAbrangenciaIds(string grupoId, string usuarioId, IEnumerable<string> abrangenciaIds)
        {
            var resultado = new List<string>();

            grupoId = grupoId.ToLower();
            usuarioId = usuarioId.ToLower();

            var query =
                new QueryContainerDescriptor<Abrangencia>().Term(t => t.Field(f => f.GrupoId.Suffix("keyword")).Value(grupoId)) &&
                new QueryContainerDescriptor<Abrangencia>().Term(t => t.Field(f => f.UsuarioId.Suffix("keyword")).Value(usuarioId));

            foreach (var abrangenciaId in abrangenciaIds)
                query &= !new QueryContainerDescriptor<Abrangencia>().Match(t => t.Field(f => f.Id).Query(abrangenciaId));

            var search = new SearchDescriptor<Abrangencia>(IndexName)
                .Query(_ => query)
                .Size(10000)
                .Scroll("10s");

            var response = await elasticClient.SearchAsync<Abrangencia>(search);

            while (response.Hits.Any())
            {
                resultado.AddRange(
                    response.Hits
                    .Where(t => t.Source.GrupoId == grupoId && t.Source.UsuarioId == usuarioId)
                    .Select(hit => hit.Source.Id)
                    .ToList());

                response = await elasticClient.ScrollAsync<Abrangencia>("10s", response.ScrollId);
            }

            return resultado;
        }

        public async Task<IEnumerable<string>> ObterIdsDiretenteGrupoIds(string[] grupoIds)
        {
            var resultado = new List<string>();

            grupoIds = grupoIds.Select(t => t.ToLower()).ToArray();

            var query = new QueryContainer();

            foreach (var grupoId in grupoIds)
                query = query && !new QueryContainerDescriptor<Abrangencia>().Term(t => t.Field(f => f.GrupoId.Suffix("keyword")).Value(grupoId));

            var search = new SearchDescriptor<Abrangencia>(IndexName)
                .Query(_ => query)
                .Size(10000)
                .Scroll("10s");

            var response = await elasticClient.SearchAsync<Abrangencia>(search);

            while (response.Hits.Any())
            {
                resultado.AddRange(
                    response.Hits
                    .Where(t => grupoIds.Contains(t.Source.GrupoId))
                    .Select(hit => hit.Source.Id)
                    .ToList());

                response = await elasticClient.ScrollAsync<Abrangencia>("10s", response.ScrollId);
            }

            return resultado;
        }

        public async Task<IEnumerable<string>> ObterIdsPorGrupoDiretenteUsuarioIds(string grupoId, string[] usuarioIds)
        {
            var resultado = new List<string>();

            grupoId = grupoId.ToLower();
            usuarioIds = usuarioIds.Select(t => t.ToLower()).ToArray();

            var query = new QueryContainerDescriptor<Abrangencia>().Term(t => t.Field(f => f.GrupoId.Suffix("keyword")).Value(grupoId));

            foreach (var usuarioId in usuarioIds)
                query = query && !new QueryContainerDescriptor<Abrangencia>().Term(t => t.Field(f => f.UsuarioId.Suffix("keyword")).Value(usuarioId));

            var search = new SearchDescriptor<Abrangencia>(IndexName)
                .Query(_ => query)
                .Size(10000)
                .Scroll("10s");

            var response = await elasticClient.SearchAsync<Abrangencia>(search);

            while (response.Hits.Any())
            {
                resultado.AddRange(
                    response.Hits
                    .Where(t => t.Source.GrupoId == grupoId && usuarioIds.Contains(t.Source.UsuarioId))
                    .Select(hit => hit.Source.Id)
                    .ToList());

                response = await elasticClient.ScrollAsync<Abrangencia>("10s", response.ScrollId);
            }

            return resultado;
        }
    }
}
