using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioAno : RepositorioBase<Ano>, IRepositorioAno
    {
        protected override string IndexName => "ano";
        public RepositorioAno(IElasticClient elasticClient) : base(elasticClient)
        {
        }
    }
}
