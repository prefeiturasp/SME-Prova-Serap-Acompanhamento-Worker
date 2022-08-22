using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories.SerapEstudantes
{
    public class RepositorioSerapAbrangencia : RepositorioSerap, IRepositorioSerapAbrangencia
    {
        public RepositorioSerapAbrangencia(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<AbrangenciaDto>> ObterPorCoressoIdAsync(Guid coressoId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"
                            select ROW_NUMBER () OVER ( ORDER by ugsc.id) as id,
                                   usc.login, 
                                   usc.nome, 
                                   gsc.id_coresso as coressoId, 
                                   gsc.nome as grupo, 
                                   a.dre_id as dreId, 
                                   a.ue_id as ueId, 
                                   a.turma_id as turmaId
                            from grupo_serap_coresso gsc 
                            left join usuario_grupo_serap_coresso ugsc on ugsc.id_grupo_serap = gsc.id
                            left join usuario_serap_coresso usc on usc.id = ugsc.id_usuario_serap 
                            left join abrangencia a on a.usuario_id = usc.id and a.grupo_id = gsc.id
                            where gsc.id_coresso = @coressoId ";

                return await conn.QueryAsync<AbrangenciaDto>(query, new { coressoId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
