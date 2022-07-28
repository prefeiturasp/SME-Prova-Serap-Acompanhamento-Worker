using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioProvaAlunoResultado : IRepositorioBase<ProvaAlunoResultado>
    {
        Task<ProvaAlunoResultado> ObterPorProvaAlunoRaAsync(long provaId, long alunoRa);
    }
}
