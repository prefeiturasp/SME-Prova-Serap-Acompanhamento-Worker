namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso
{
    public class GrupoExcluirCoressoDto
    {
        public GrupoExcluirCoressoDto() { }
        public GrupoExcluirCoressoDto(string grupoId, string[] usuarioIds)
        {
            GrupoId = grupoId;
            UsuarioIds = usuarioIds;
        }

        public string GrupoId { get; set; }
        public string[] UsuarioIds { get; set; }
    }
}
