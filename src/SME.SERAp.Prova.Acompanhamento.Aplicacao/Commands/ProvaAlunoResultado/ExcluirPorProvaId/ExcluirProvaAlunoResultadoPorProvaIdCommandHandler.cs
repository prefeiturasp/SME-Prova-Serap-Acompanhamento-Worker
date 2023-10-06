using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirProvaAlunoResultadoPorProvaIdCommandHandler : IRequestHandler<ExcluirProvaAlunoResultadoPorProvaIdCommand, bool>
    {
        private readonly IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado;

        public ExcluirProvaAlunoResultadoPorProvaIdCommandHandler(IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado)
        {
            this.repositorioProvaAlunoResultado = repositorioProvaAlunoResultado ?? throw new ArgumentNullException(nameof(repositorioProvaAlunoResultado));
        }

        public async Task<bool> Handle(ExcluirProvaAlunoResultadoPorProvaIdCommand request, CancellationToken cancellationToken)
        {
            return await repositorioProvaAlunoResultado.DeletarPorProvaIdAsync(request.ProvaId);
        }
    }
}
