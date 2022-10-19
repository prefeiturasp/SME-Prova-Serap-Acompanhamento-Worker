using MediatR;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaIdsDiferenteGrupoIdsQuery : IRequest<IEnumerable<string>>
    {
        public ObterAbrangenciaIdsDiferenteGrupoIdsQuery(string[] grupoIds)
        {
            GrupoIds = grupoIds;
        }

        public string[] GrupoIds { get; set; }
    }
}
