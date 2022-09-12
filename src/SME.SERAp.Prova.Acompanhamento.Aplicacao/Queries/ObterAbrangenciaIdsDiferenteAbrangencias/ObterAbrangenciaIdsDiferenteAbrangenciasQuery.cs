using MediatR;
using System;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaIdsDiferenteAbrangenciasQuery : IRequest<IEnumerable<string>>
    {
        public ObterAbrangenciaIdsDiferenteAbrangenciasQuery(Guid grupoId, Guid usuarioId, IEnumerable<string> abrangencias)
        {
            GrupoId = grupoId;
            UsuarioId = usuarioId;
            Abrangencias = abrangencias;
        }

        public Guid GrupoId { get; set; }
        public Guid UsuarioId { get; set; }
        public IEnumerable<string> Abrangencias { get; set; }
    }
}
