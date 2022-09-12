using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioTurma : IRepositorioBase<Turma>
    {
        Task<Turma> ObterPorCodigoAsync(int ano, long codigo);
    }
}
