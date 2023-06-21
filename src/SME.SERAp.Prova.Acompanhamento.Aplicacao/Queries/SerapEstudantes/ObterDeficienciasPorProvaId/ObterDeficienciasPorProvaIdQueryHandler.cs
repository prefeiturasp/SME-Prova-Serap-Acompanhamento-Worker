using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterDeficienciasPorProvaIdQueryHandler : IRequestHandler<ObterDeficienciasPorProvaIdQuery, IEnumerable<long>>
    {
        private readonly IRepositorioSerapProva repositorioSerapProva;

        public ObterDeficienciasPorProvaIdQueryHandler(IRepositorioSerapProva repositorioSerapProva)
        {
            this.repositorioSerapProva = repositorioSerapProva ?? throw new ArgumentNullException(nameof(repositorioSerapProva));
        }

        public async Task<IEnumerable<long>> Handle(ObterDeficienciasPorProvaIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSerapProva.ObterDeficienciasAsync(request.ProvaId);
        }
    }
}
