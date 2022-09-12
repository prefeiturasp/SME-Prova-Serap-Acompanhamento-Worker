using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioAbrangencia : IRepositorioBase<Abrangencia>
    {
        Task<IEnumerable<string>> ObterIdsPorGrupoDiretenteUsuarioIds(Guid grupoId, Guid[] usuarioIds);
        Task<IEnumerable<string>> ObterIdsDiretenteGrupoIds(Guid[] grupoIds);
        Task<IEnumerable<string>> ObterIdsPorGrupoUsuarioDiretenteAbrangenciaIds(Guid grupoId, Guid usuarioId, IEnumerable<string> abrangenciaIds);
    }
}
