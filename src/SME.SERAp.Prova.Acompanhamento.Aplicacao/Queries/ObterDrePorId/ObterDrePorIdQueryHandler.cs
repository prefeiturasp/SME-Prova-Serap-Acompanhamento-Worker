using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterDrePorIdQueryHandler : IRequestHandler<ObterDrePorIdQuery, Dre>
    {
        private readonly IRepositorioDre repositorioDre;

        public ObterDrePorIdQueryHandler(IRepositorioDre repositorioDre)
        {
            this.repositorioDre = repositorioDre ?? throw new ArgumentException(nameof(repositorioDre));
        }

        public Task<Dre> Handle(ObterDrePorIdQuery request, CancellationToken cancellationToken)
        {
            return repositorioDre.ObterPorIdAsync(request.Id);
        }
    }
}
