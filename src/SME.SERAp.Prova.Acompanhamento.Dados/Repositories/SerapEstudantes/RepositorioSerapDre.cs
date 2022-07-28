using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories.SerapEstudantes
{
    public class RepositorioSerapDre : RepositorioSerap, IRepositorioSerapDre
    {
        public RepositorioSerapDre(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<DreDto>> ObterTodosAsync()
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select 
	                                id, 
	                                dre_id as codigo, 
	                                abreviacao,
	                                nome
                              from dre";

                return await conn.QueryAsync<DreDto>(query);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
