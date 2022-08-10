using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries.SerapEstudantes.ObterSituacaoTurmaProvaSerap
{
    public class ObterSituacaoTurmaProvaSerapQueryHandler : IRequestHandler<ObterSituacaoTurmaProvaSerapQuery, SituacaoTurmaProvaDto>
    {
        private readonly IRepositorioSerapProva repositorioSerapProva;

        public ObterSituacaoTurmaProvaSerapQueryHandler(IRepositorioSerapProva repositorioSerapProva)
        {
            this.repositorioSerapProva = repositorioSerapProva ?? throw new ArgumentNullException(nameof(repositorioSerapProva));
        }

        public async Task<SituacaoTurmaProvaDto> Handle(ObterSituacaoTurmaProvaSerapQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSerapProva.ObterSituacaoTurmaAsync(request.ProvaId, request.TurmaId);
        }
    }
}
