using MediatR;
using System;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaIdsPorGrupoDiferenteUsuarioIdsQuery : IRequest<IEnumerable<string>>
    {
        public ObterAbrangenciaIdsPorGrupoDiferenteUsuarioIdsQuery(Guid grupoId, Guid[] usuarioIds)
        {
            GrupoId = grupoId;
            UsuarioIds = usuarioIds;
        }

        public Guid GrupoId { get; set; }
        public Guid[] UsuarioIds { get; set; }
    }
}
