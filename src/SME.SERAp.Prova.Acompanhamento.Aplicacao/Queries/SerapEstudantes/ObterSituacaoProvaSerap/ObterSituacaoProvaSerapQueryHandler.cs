using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterSituacaoProvaSerapQueryHandler : IRequestHandler<ObterSituacaoAlunoProvaSerapQuery, SituacaoAlunoProvaDto>
    {
        private readonly IRepositorioSerapProva repositorioSerapProva;

        public ObterSituacaoProvaSerapQueryHandler(IRepositorioSerapProva repositorioSerapProva)
        {
            this.repositorioSerapProva = repositorioSerapProva ?? throw new ArgumentNullException(nameof(repositorioSerapProva));
        }

        public async Task<SituacaoAlunoProvaDto> Handle(ObterSituacaoAlunoProvaSerapQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSerapProva.ObterSituacaoAlunoAsync(request.ProvaId, request.Ra);
        }
    }
}
