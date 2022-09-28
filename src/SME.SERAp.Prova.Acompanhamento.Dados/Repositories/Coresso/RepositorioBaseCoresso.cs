using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories.Coresso
{
    public abstract class RepositorioBaseCoresso
    {
        private readonly ConnectionStringOptions connectionStrings;

        public RepositorioBaseCoresso(ConnectionStringOptions connectionStrings)
        {
            this.connectionStrings = connectionStrings ?? throw new ArgumentNullException(nameof(connectionStrings));
        }

        protected IDbConnection ObterConexao()
        {
            var conexao = new SqlConnection(connectionStrings.CoreSSO);
            conexao.Open();
            return conexao;
        }
    }
}
