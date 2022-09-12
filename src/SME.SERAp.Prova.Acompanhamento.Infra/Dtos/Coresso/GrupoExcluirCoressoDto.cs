using System;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso
{
    public class GrupoExcluirCoressoDto
    {
        public GrupoExcluirCoressoDto() { }
        public GrupoExcluirCoressoDto(Guid grupoId, Guid[] usuarioIds)
        {
            GrupoId = grupoId;
            UsuarioIds = usuarioIds;
        }

        public Guid GrupoId { get; set; }
        public Guid[] UsuarioIds { get; set; }
    }
}
