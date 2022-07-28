using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioTurma : RepositorioBase<Turma>, IRepositorioTurma
    {
        protected override string IndexName => "turma";
        public RepositorioTurma(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}
