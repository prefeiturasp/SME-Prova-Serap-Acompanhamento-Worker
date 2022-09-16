using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioAbrangencia : IRepositorioBase<Abrangencia>
    {
        Task<IEnumerable<string>> ObterIdsPorGrupoDiretenteUsuarioIds(string grupoId, string[] usuarioIds);
        Task<IEnumerable<string>> ObterIdsDiretenteGrupoIds(string[] grupoIds);
        Task<IEnumerable<string>> ObterIdsPorGrupoUsuarioDiretenteAbrangenciaIds(string grupoId, string usuarioId, IEnumerable<string> abrangenciaIds);
    }
}
