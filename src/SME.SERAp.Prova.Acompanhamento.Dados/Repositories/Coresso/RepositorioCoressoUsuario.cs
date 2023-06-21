using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.Coresso;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories.Coresso
{
    public class RepositorioCoressoUsuario : RepositorioBaseCoresso, IRepositorioCoressoUsuario
    {
        public RepositorioCoressoUsuario(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<UsuarioCoressoDto>> ObterUsuariosPorSistemaGrupo(long sistemaId, Guid grupoId)
        {
            var conn = ObterConexao();
            try
            {
                var query = @"select distinct 
	                            u.usu_id as Id, 
	                            u.usu_login as [Login], 
	                            p.pes_nome as Nome
                              from SYS_Grupo g
                              inner join SYS_UsuarioGrupo ug on g.gru_id = ug.gru_id
                              inner join SYS_Usuario u on u.usu_id = ug.usu_id
                              left join PES_Pessoa p on u.pes_id = p.pes_id
                              where g.gru_situacao = 1 
                                and ug.usg_situacao = 1
                                and u.usu_situacao = 1
                                and g.sis_id = @sistemaId
                                and g.gru_id = @grupoId
                              order by p.pes_nome";

                return await conn.QueryAsync<UsuarioCoressoDto>(query, new { sistemaId, grupoId });
            }
            finally
            {
                conn.Dispose();
            }
        }
    }
}
