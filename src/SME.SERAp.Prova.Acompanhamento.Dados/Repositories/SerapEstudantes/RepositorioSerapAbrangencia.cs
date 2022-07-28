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
                var query = @"select vaug.id, 
                                     vaug.login, 
                                     vaug.usuario, 
                                     vaug.id_coresso as coressoId,  
                                     vaug.grupo, 
                                     vaug.dre_id as dreId, 
                                     vaug.ue_id as ueId, 
                                     vaug.turma_id as turmaId
                              from v_abrangencia_usuario_grupo vaug where vaug.id_coresso = @coressoId ";

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
