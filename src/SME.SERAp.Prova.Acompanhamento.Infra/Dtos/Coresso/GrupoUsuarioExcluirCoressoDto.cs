using System;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso
{
    public class GrupoUsuarioExcluirCoressoDto
    {
        public GrupoUsuarioExcluirCoressoDto() { }
        public GrupoUsuarioExcluirCoressoDto(Guid grupoId, Guid usuarioId, IEnumerable<string> abrangenciaIds)
        {
            GrupoId = grupoId;
            UsuarioId = usuarioId;
            AbrangenciaIds = abrangenciaIds;
        }

        public Guid GrupoId { get; set; }
        public Guid UsuarioId { get; set; }
        public IEnumerable<string> AbrangenciaIds { get; set; }
    }
}
