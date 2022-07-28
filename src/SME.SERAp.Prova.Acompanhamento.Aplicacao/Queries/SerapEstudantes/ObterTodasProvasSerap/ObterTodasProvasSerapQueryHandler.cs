using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTodasProvasSerapQueryHandler : IRequestHandler<ObterTodasProvasSerapQuery, IEnumerable<ProvaDto>>
    {
        private readonly IRepositorioSerapProva repositorioSerapProva;

        public ObterTodasProvasSerapQueryHandler(IRepositorioSerapProva repositorioSerapProva)
        {
            this.repositorioSerapProva = repositorioSerapProva ?? throw new ArgumentNullException(nameof(repositorioSerapProva));
        }

        public async Task<IEnumerable<ProvaDto>> Handle(ObterTodasProvasSerapQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSerapProva.ObterTodosAsync();
        }
    }
}
