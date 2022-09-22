using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
   public class ExcluirProvaAlunoResultadoCommand : IRequest<bool>
    {
        public ExcluirProvaAlunoResultadoCommand(string provaAlunoResultadoId)
        {
            ProvaAlunoResultadoId = provaAlunoResultadoId;
        }

        public string ProvaAlunoResultadoId { get; set; }
    }
}