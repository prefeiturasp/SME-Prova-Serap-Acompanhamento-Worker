using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvasParaDeficientesQueryHandler : IRequestHandler<ObterProvasParaDeficientesQuery, IEnumerable<ProvaDto>>
    {
        private readonly IRepositorioSerapProva repositorioSerapProva;

        public ObterProvasParaDeficientesQueryHandler(IRepositorioSerapProva repositorioSerapProva)
        {
            this.repositorioSerapProva = repositorioSerapProva ?? throw new ArgumentNullException(nameof(repositorioSerapProva));
        }

        public async Task<IEnumerable<ProvaDto>> Handle(ObterProvasParaDeficientesQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSerapProva.ObterProvasParaDeficientesAsync();
        }
    }
}