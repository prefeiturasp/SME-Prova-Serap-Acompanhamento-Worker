using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.Coresso
{
    public interface IRepositorioCoressoAbrangencia
    {
        Task<IEnumerable<string>> ObterAbrangenciaPorGrupoUsuario(Guid grupoId, Guid usuarioId);
    }
}
