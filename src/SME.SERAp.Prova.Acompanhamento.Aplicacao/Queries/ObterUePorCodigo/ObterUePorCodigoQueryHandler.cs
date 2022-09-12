using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterUePorCodigoQueryHandler : IRequestHandler<ObterUePorCodigoQuery, Ue>
    {
        private readonly IRepositorioUe repositorioUe;

        public ObterUePorCodigoQueryHandler(IRepositorioUe repositorioUe)
        {
            this.repositorioUe = repositorioUe ?? throw new ArgumentNullException(nameof(repositorioUe));
        }

        public async Task<Ue> Handle(ObterUePorCodigoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioUe.ObterPorCodigoAsync(request.Codigo);
        }
    }
}
