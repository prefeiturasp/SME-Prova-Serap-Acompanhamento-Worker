using Npgsql;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System;
using System.Data;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public abstract class RepositorioSerap
    {
        private readonly ConnectionStringOptions connectionStrings;

        public RepositorioSerap(ConnectionStringOptions connectionStrings)
        {
            this.connectionStrings = connectionStrings ?? throw new ArgumentNullException(nameof(connectionStrings));
        }

        protected IDbConnection ObterConexao()
        {
            var conexao = new NpgsqlConnection(connectionStrings.ApiSerap);
            conexao.Open();
            return conexao;
        }
    }
}
