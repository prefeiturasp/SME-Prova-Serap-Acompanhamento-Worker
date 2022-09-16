using MediatR;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaIdsPorGrupoDiferenteUsuarioIdsQuery : IRequest<IEnumerable<string>>
    {
        public ObterAbrangenciaIdsPorGrupoDiferenteUsuarioIdsQuery(string grupoId, string[] usuarioIds)
        {
            GrupoId = grupoId;
            UsuarioIds = usuarioIds;
        }

        public string GrupoId { get; set; }
        public string[] UsuarioIds { get; set; }
    }
}
