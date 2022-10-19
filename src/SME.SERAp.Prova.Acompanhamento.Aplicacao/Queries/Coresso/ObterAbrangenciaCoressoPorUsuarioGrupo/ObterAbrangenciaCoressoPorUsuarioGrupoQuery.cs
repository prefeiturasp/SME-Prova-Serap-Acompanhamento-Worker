using MediatR;
using System;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaCoressoPorUsuarioGrupoQuery : IRequest<IEnumerable<string>>
    {
        public ObterAbrangenciaCoressoPorUsuarioGrupoQuery(Guid grupoId, Guid usuarioId)
        {
            GrupoId = grupoId;
            UsuarioId = usuarioId;
        }

        public Guid GrupoId { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
