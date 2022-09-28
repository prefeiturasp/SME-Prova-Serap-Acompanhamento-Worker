using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories.Eol
{
    public abstract class RepositorioBaseEol
    {
        private readonly ConnectionStringOptions connectionStrings;

        public RepositorioBaseEol(ConnectionStringOptions connectionStrings)
        {
            this.connectionStrings = connectionStrings ?? throw new ArgumentNullException(nameof(connectionStrings));
        }

        protected IDbConnection ObterConexao()
        {
            var conexao = new SqlConnection(connectionStrings.Eol);
            conexao.Open();
            return conexao;
        }
    }
}
