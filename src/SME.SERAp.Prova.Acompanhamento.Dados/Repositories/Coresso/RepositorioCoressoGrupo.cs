using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.Coresso;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories.Coresso
{
    public class RepositorioCoressoGrupo : RepositorioBaseCoresso, IRepositorioCoressoGrupo
    {
        public RepositorioCoressoGrupo(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<GrupoCoressoDto>> ObterGruposPorSistemaModulo(long sistemaId, long moduloId)
        {
            var conn = ObterConexao();
            try
            {
                var query = @"select distinct 
	                        gp.gru_id as Id, 
	                        g.gru_nome as Nome, 
	                        gp.grp_consultar as PermiteConsultar, 
	                        gp.grp_alterar as PermiteAlterar
                          from SYS_GrupoPermissao gp
                          left join SYS_Grupo g on g.gru_id = gp.gru_id
                          where g.gru_situacao = 1
                            and gp.sis_id = @sistemaId 
                            and gp.mod_id = @moduloId 
                          order by g.gru_nome";

                return await conn.QueryAsync<GrupoCoressoDto>(query, new { sistemaId, moduloId });
            }
            finally
            {
                conn.Dispose();
            }
        }
    }
}
