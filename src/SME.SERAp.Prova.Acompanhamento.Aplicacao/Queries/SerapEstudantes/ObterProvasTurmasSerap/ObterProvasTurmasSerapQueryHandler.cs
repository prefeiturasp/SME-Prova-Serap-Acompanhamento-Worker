using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvasTurmasSerapQueryHandler : IRequestHandler<ObterProvasTurmasSerapQuery, IEnumerable<ProvaTurmaDto>>
    {
        private readonly IRepositorioSerapProva repositorioSerapProva;

        public ObterProvasTurmasSerapQueryHandler(IRepositorioSerapProva repositorioSerapProva)
        {
            this.repositorioSerapProva = repositorioSerapProva ?? throw new ArgumentNullException(nameof(repositorioSerapProva));
        }

        public Task<IEnumerable<ProvaTurmaDto>> Handle(ObterProvasTurmasSerapQuery request, CancellationToken cancellationToken)
        {
            return repositorioSerapProva.ObterTurmasAsync(request.ProvaId);
        }
    }
}
