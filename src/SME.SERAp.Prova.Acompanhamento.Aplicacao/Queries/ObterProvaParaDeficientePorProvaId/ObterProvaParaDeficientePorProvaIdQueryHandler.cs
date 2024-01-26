using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaParaDeficientePorProvaIdQueryHandler : IRequestHandler<ObterProvaParaDeficientePorProvaIdQuery, ProvaDto>
    {
        private readonly IRepositorioSerapProva repositorioSerapProva;

        public ObterProvaParaDeficientePorProvaIdQueryHandler(IRepositorioSerapProva repositorioSerapProva)
        {
            this.repositorioSerapProva = repositorioSerapProva ?? throw new ArgumentNullException(nameof(repositorioSerapProva));
        }

        public async Task<ProvaDto> Handle(ObterProvaParaDeficientePorProvaIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSerapProva.ObterProvaParaDeficientePorProvaIdAsync(request.ProvaId);
        }
    }
}