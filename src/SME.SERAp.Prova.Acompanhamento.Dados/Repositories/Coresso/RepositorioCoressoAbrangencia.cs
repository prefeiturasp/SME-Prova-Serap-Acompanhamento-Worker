using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.Coresso;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories.Coresso
{
    public class RepositorioCoressoAbrangencia : RepositorioBaseCoresso, IRepositorioCoressoAbrangencia
    {
        public RepositorioCoressoAbrangencia(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<string>> ObterAbrangenciaPorGrupoUsuario(Guid grupoId, Guid usuarioId)
        {
            var conn = ObterConexao();
            try
            {
                var query = @"select distinct ua.uad_codigo
                              from SYS_UsuarioGrupoUA ugua
                              left join SYS_UnidadeAdministrativa ua ON ugua.uad_id = ua.uad_id
                              where ua.uad_situacao = 1
                                and ugua.gru_id = @grupoId
                                and ugua.usu_id = @usuarioId";

                return await conn.QueryAsync<string>(query, new { grupoId, usuarioId });
            }
            finally
            {
                conn.Dispose();
            }
        }
    }
}
