using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioTurma : RepositorioBase<Turma>, IRepositorioTurma
    {
        public RepositorioTurma(ElasticOptions elasticOptions, IElasticClient elasticClient) : base(elasticOptions, elasticClient)
        {
        }
    }
}
