using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioUe : IRepositorioBase<Ue>
    {
        Task<Ue> ObterPorCodigoAsync(long codigo);
    }
}
