using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTodasUesPorDreIdSerapQueryHandler : IRequestHandler<ObterTodasUesPorDreIdSerapQuery, IEnumerable<UeDto>>
    {
        private readonly IRepositorioSerapUe repositorioSerapUe;

        public ObterTodasUesPorDreIdSerapQueryHandler(IRepositorioSerapUe repositorioSerapUe)
        {
            this.repositorioSerapUe = repositorioSerapUe ?? throw new ArgumentNullException(nameof(repositorioSerapUe));
        }

        public async Task<IEnumerable<UeDto>> Handle(ObterTodasUesPorDreIdSerapQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSerapUe.ObterPorDreIdAsync(request.DreId);
        }
    }
}
