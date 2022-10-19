using System;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes
{
    public class AbrangenciaDto
    {
        public Guid UsuarioId { get; set; }
        public string Login { get; set; }
        public string Usuario { get; set; }
        public Guid GrupoId { get; set; }
        public string Grupo { get; set; }
        public bool PermiteConsultar { get; set; }
        public bool PermiteAlterar { get; set; }
        public long DreId { get; set; }
        public long UeId { get; set; }
        public long TurmaId { get; set; }
    }
}
