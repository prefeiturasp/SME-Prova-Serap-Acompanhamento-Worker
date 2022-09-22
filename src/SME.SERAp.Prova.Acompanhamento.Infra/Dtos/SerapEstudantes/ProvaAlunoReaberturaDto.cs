using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes
{
  public   class ProvaAlunoReaberturaDto
    {
        public long ProvaId { get; set; }
        public long AlunoRa { get; set; }
        public string LoginCoresso { get; set; }
        public string UsuarioCoresso { get; set; }
        public Guid GrupoCoresso { get; set; }
    }
}
