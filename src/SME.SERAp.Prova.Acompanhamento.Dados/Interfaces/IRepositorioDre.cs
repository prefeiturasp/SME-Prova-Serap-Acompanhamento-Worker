using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioDre : IRepositorioBase<Dre>
    {
        Task<Dre> ObterPorCodigoAsync(long codigo);
    }
}
