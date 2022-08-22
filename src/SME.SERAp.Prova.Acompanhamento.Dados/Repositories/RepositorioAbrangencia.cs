using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioAbrangencia : RepositorioBase<Abrangencia>, IRepositorioAbrangencia
    {
        protected override string IndexName => "abrangencia";
        public RepositorioAbrangencia(IElasticClient elasticClient) : base(elasticClient)
        {
        }

        public async Task<Abrangencia> ObterPorIdAsync(string id)
        {
            var response = await elasticClient.GetAsync(DocumentPath<Abrangencia>.Id(id).Index(IndexName));

            if (!response.IsValid) return default;

            return response.Source;
        }
    }
}
