using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirProvaAlunoRespostaPorAlunoCommandHandler : IRequestHandler<ExcluirProvaAlunoRespostaPorAlunoCommand, bool>
    {
        private readonly IRepositorioProvaAlunoResposta repositorioProvaAlunoResposta;
        public ExcluirProvaAlunoRespostaPorAlunoCommandHandler(IRepositorioProvaAlunoResposta repositorioProvaAlunoResposta)
        {
            this.repositorioProvaAlunoResposta = repositorioProvaAlunoResposta;
        }

        public Task<bool> Handle(ExcluirProvaAlunoRespostaPorAlunoCommand request, CancellationToken cancellationToken)
        {
            return repositorioProvaAlunoResposta.DeletarPorAlunoProvaAsync(request.ProvaId, request.AlunoRa);
        }
    }
}
