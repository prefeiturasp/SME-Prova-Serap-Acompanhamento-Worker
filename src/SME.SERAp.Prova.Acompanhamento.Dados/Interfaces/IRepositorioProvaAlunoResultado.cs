using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioProvaAlunoResultado : IRepositorioBase<ProvaAlunoResultado>
    {
        Task<ProvaAlunoResultado> ObterPorProvaAlunoIdAsync(long provaId, long alunoId);
        Task<IEnumerable<ProvaAlunoResultado>> ObterPorProvaAlunoRaAsync(long provaId, long alunoRa);
    }
}
