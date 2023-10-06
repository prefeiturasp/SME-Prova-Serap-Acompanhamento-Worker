using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirProvaCommandHandler : IRequestHandler<ExcluirProvaCommand, bool>
    {
        private readonly IRepositorioProva repositorioProva;

        public ExcluirProvaCommandHandler(IRepositorioProva repositorioProva)
        {
            this.repositorioProva = repositorioProva ?? throw new ArgumentNullException(nameof(repositorioProva));
        }

        public Task<bool> Handle(ExcluirProvaCommand request, CancellationToken cancellationToken)
        {
            return repositorioProva.DeletarAsync(request.Id);
        }
    }
}
