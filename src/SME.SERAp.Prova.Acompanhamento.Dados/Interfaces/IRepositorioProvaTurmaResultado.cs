using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioProvaTurmaResultado : IRepositorioBase<ProvaTurmaResultado>
    {
        Task<ProvaTurmaResultado> ObterPorProvaTurmaAsync(long provaId, long turmaId);
    }
}
