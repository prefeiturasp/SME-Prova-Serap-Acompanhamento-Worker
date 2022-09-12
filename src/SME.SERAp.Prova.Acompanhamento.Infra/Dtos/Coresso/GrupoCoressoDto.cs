using System;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso
{
    public class GrupoCoressoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool PermiteConsultar { get; set; }
        public bool PermiteAlterar { get; set; }
    }
}
