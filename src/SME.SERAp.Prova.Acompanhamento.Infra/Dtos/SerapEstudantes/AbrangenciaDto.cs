using System;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes
{
    public class AbrangenciaDto
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Usuario { get; set; }
        public Guid CoressoId { get; set; }
        public string Grupo { get; set; }
        public long DreId { get; set; }
        public long UeId { get; set; }
        public long TurmaId { get; set; }
    }
}
