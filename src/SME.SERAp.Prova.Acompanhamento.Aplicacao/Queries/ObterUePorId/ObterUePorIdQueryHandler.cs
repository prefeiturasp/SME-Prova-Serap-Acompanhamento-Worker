using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterUePorIdQueryHandler : IRequestHandler<ObterUePorIdQuery, Ue>
    {
        private readonly IRepositorioUe repositorioUe;

        public ObterUePorIdQueryHandler(IRepositorioUe repositorioUe)
        {
            this.repositorioUe = repositorioUe ?? throw new ArgumentNullException(nameof(repositorioUe));
        }

        public Task<Ue> Handle(ObterUePorIdQuery request, CancellationToken cancellationToken)
        {
            return repositorioUe.ObterPorIdAsync(request.Id);
        }
    }
}
