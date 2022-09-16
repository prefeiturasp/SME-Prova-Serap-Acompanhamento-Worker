using MediatR;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaIdsDiferenteAbrangenciasQuery : IRequest<IEnumerable<string>>
    {
        public ObterAbrangenciaIdsDiferenteAbrangenciasQuery(string grupoId, string usuarioId, IEnumerable<string> abrangencias)
        {
            GrupoId = grupoId;
            UsuarioId = usuarioId;
            Abrangencias = abrangencias;
        }

        public string GrupoId { get; set; }
        public string UsuarioId { get; set; }
        public IEnumerable<string> Abrangencias { get; set; }
    }
}
