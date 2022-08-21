using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes
{
    public class ProvaDto
    {
        public long Id { get; set; }
        public long Codigo { get; set; }
        public string Descricao { get; set; }
        public Modalidade Modalidade { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
    }
}
