namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso
{
    public class GrupoUsuarioCoressoDto
    {
        public GrupoUsuarioCoressoDto() { }
        public GrupoUsuarioCoressoDto(GrupoCoressoDto grupo, UsuarioCoressoDto usuario)
        {
            Grupo = grupo;
            Usuario = usuario;
        }

        public GrupoCoressoDto Grupo { get; set; }
        public UsuarioCoressoDto Usuario { get; set; }
    }
}
