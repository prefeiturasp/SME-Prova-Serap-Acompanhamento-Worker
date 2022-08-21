using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories.SerapEstudantes
{
    public class RepositorioSerapAno : RepositorioSerap, IRepositorioSerapAno
    {
        public RepositorioSerapAno(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<AnoDto>> ObterTodosAsync()
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select ROW_NUMBER () OVER ( ORDER BY ano_letivo, ue_id, ano) as id,
                                modalidade_codigo as modalidade,
                                ano_letivo as anoLetivo,
	                            ue_id as UeId,
	                            ano as Nome
                              from turma
                              group by ano_letivo, modalidade_codigo, ue_id, ano";

                return await conn.QueryAsync<AnoDto>(query);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
