using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterDrePorCodigoQueryHandler : IRequestHandler<ObterDrePorCodigoQuery, Dre>
    {
        private readonly IRepositorioDre repositorioDre;

        public ObterDrePorCodigoQueryHandler(IRepositorioDre repositorioDre)
        {
            this.repositorioDre = repositorioDre ?? throw new ArgumentNullException(nameof(repositorioDre));
        }

        public async Task<Dre> Handle(ObterDrePorCodigoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioDre.ObterPorCodigoAsync(request.Codigo);
        }
    }
}
