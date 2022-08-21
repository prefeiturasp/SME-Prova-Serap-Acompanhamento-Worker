using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirProvaAlunoResultadoCommandHandler : IRequestHandler<InserirProvaAlunoResultadoCommand, bool>
    {
        private readonly IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado;

        public InserirProvaAlunoResultadoCommandHandler(IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado)
        {
            this.repositorioProvaAlunoResultado = repositorioProvaAlunoResultado ?? throw new ArgumentNullException(nameof(repositorioProvaAlunoResultado));
        }

        public async Task<bool> Handle(InserirProvaAlunoResultadoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioProvaAlunoResultado.InserirAsync(request.ProvaTurmaAlunoSituacao);
        }
    }
}
