using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioProvaAlunoResultado : IRepositorioBase<ProvaAlunoResultado>
    {
        Task<bool> DeletarPorProvaIdAsync(long provaId);
        Task<RetornoPaginadoDto<ProvaAlunoResultado>> ObterPaginadoAsync(long provaId, long turmaId, string scrollId);
        Task<ProvaAlunoResultado> ObterPorProvaAlunoIdAsync(long provaId, long alunoId);
        Task<IEnumerable<ProvaAlunoResultado>> ObterPorProvaAlunoRaAsync(long provaId, long alunoRa);
        Task<IEnumerable<ProvaAlunoResultado>> ObterPorProvaTurmaIdAsync(long provaId, long turmaId);
        Task<IEnumerable<ProvaAlunoResultado>> ObterPorTurmaIdAsync(long turmaId);
        Task<IEnumerable<ProvaAlunoResultado>> ObterPorProvaIdAsync(long provaId);
    }
}
