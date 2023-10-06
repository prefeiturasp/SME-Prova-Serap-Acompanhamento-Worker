using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirProvaAlunoRespostaCommandHandler : IRequestHandler<ExcluirProvaAlunoRespostaPorProvaIdCommand, bool>
    {
        private readonly IRepositorioProvaAlunoResposta repositorioProvaAlunoResposta;

        public ExcluirProvaAlunoRespostaCommandHandler(IRepositorioProvaAlunoResposta repositorioProvaAlunoResposta)
        {
            this.repositorioProvaAlunoResposta = repositorioProvaAlunoResposta ?? throw new ArgumentNullException(nameof(repositorioProvaAlunoResposta));
        }

        public async Task<bool> Handle(ExcluirProvaAlunoRespostaPorProvaIdCommand request, CancellationToken cancellationToken)
        {
            return await repositorioProvaAlunoResposta.DeletarPorProvaIdAsync(request.ProvaId);
        }
    }
}
