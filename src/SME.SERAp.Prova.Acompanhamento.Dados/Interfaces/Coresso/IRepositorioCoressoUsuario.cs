using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.Coresso
{
    public interface IRepositorioCoressoUsuario
    {
        Task<IEnumerable<UsuarioCoressoDto>> ObterUsuariosPorSistemaGrupo(long sistemaId, Guid grupoId);
    }
}
