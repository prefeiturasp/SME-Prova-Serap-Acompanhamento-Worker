using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories.SerapEstudantes
{
    public class RepositorioSerapUe : RepositorioSerap, IRepositorioSerapUe
    {
        public RepositorioSerapUe(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<UeDto>> ObterPorDreIdAsync(long dreId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id,
                                     ue_id as codigo,
                                     dre_id as dreId,
                                     nome
                              from Ue
                              where dre_id = @dreId";

                return await conn.QueryAsync<UeDto>(query, new { dreId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
