using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirAbrangenciaCommandHandler : IRequestHandler<ExcluirAbrangenciaCommand, bool>
    {
        private readonly IRepositorioAbrangencia repositorioAbrangencia;

        public ExcluirAbrangenciaCommandHandler(IRepositorioAbrangencia repositorioAbrangencia)
        {
            this.repositorioAbrangencia = repositorioAbrangencia ?? throw new ArgumentNullException(nameof(repositorioAbrangencia));
        }

        public async Task<bool> Handle(ExcluirAbrangenciaCommand request, CancellationToken cancellationToken)
        {
            return await repositorioAbrangencia.DeletarAsync(request.Id);
        }
    }
}
