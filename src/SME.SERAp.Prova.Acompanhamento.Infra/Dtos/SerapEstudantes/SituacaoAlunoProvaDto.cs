using System;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes
{
    public class SituacaoAlunoProvaDto
    {
        public bool FezDownload { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public int? Tempo { get; set; }
        public int? QuestaoRespondida { get; set; }
        public string UsuarioIdReabertura { get; set;} 
        public DateTime? DataHoraReabertura { get; set; }
    }
}
