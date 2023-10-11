using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioProvaTurmaResultado : IRepositorioBase<ProvaTurmaResultado>
    {
        Task<ProvaTurmaResultado> ObterPorProvaTurmaAsync(long provaId, long turmaId);
        Task<bool> ExcluirPorProvaTurmaIdAsync(ProvaTurmaResultado provaTurmaResultado);
        Task<bool> ExcluirPorProvaIdAsync(long provaId);
        Task<RetornoPaginadoDto<ProvaTurmaResultado>> ObterPaginadoAsync(long provaId, string scrollId);
    }
}
