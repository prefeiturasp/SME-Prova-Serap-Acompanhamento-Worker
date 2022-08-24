using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvasEmAndamentoQueryHandler : IRequestHandler<ObterProvasEmAndamentoQuery, IEnumerable<ProvaDto>>
    {
        private readonly IRepositorioSerapProva repositorioSerapProva;

        public ObterProvasEmAndamentoQueryHandler(IRepositorioSerapProva repositorioSerapProva)
        {
            this.repositorioSerapProva = repositorioSerapProva;
        }

        public async Task<IEnumerable<ProvaDto>> Handle(ObterProvasEmAndamentoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSerapProva.ObterProvasEmAndamentoAsync();
        }
    }
}
