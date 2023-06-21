using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso;
using System;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterUsuariosCoressoPorGrupoQuery : IRequest<IEnumerable<UsuarioCoressoDto>>
    {
        public ObterUsuariosCoressoPorGrupoQuery(Guid grupoId)
        {
            GrupoId = grupoId;
        }

        public Guid GrupoId { get; set; }
    }
}
