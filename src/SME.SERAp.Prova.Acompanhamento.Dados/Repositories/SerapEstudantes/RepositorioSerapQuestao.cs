using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories.SerapEstudantes
{
    public class RepositorioSerapQuestao : RepositorioSerap, IRepositorioSerapQuestao
    {
        public RepositorioSerapQuestao(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {

        }

        public async Task<IEnumerable<long>> ObterQuestoesIdsPorProvaId(long provaId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id 
                              from questao q where q.prova_id = @provaId";

                return await conn.QueryAsync<long>(query, new { provaId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
