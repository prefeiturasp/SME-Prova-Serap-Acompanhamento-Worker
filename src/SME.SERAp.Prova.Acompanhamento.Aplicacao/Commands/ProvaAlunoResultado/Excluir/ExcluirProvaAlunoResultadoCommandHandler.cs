using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao 
{ 
  public  class ExcluirProvaAlunoResultadoCommandHandler : IRequestHandler<ExcluirProvaAlunoResultadoCommand, bool>
    {
        private readonly IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado;

        public ExcluirProvaAlunoResultadoCommandHandler(IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado)
        {
            this.repositorioProvaAlunoResultado = repositorioProvaAlunoResultado ?? throw new ArgumentNullException(nameof(repositorioProvaAlunoResultado));
        }

        public async Task<bool> Handle(ExcluirProvaAlunoResultadoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioProvaAlunoResultado.DeletarAsync(request.ProvaAlunoResultadoId);
        }
    }
}