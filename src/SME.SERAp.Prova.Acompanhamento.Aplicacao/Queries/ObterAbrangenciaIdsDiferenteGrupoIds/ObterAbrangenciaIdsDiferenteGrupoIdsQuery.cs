using MediatR;
using System;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaIdsDiferenteGrupoIdsQuery : IRequest<IEnumerable<string>>
    {
        public ObterAbrangenciaIdsDiferenteGrupoIdsQuery(Guid[] grupoIds)
        {
            GrupoIds = grupoIds;
        }

        public Guid[] GrupoIds { get; set; }
    }
}
