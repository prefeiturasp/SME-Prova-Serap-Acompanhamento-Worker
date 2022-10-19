using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso
{
    public class GrupoUsuarioExcluirCoressoDto
    {
        public GrupoUsuarioExcluirCoressoDto() { }
        public GrupoUsuarioExcluirCoressoDto(string grupoId, string usuarioId, IEnumerable<string> abrangenciaIds)
        {
            GrupoId = grupoId;
            UsuarioId = usuarioId;
            AbrangenciaIds = abrangenciaIds;
        }

        public string GrupoId { get; set; }
        public string UsuarioId { get; set; }
        public IEnumerable<string> AbrangenciaIds { get; set; }
    }
}
