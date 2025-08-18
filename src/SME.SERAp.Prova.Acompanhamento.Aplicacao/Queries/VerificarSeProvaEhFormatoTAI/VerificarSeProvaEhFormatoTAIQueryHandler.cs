using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries
{
    public class VerificarSeProvaEhFormatoTAIQueryHandler : IRequestHandler<VerificarSeProvaEhFormatoTAIQuery, bool>
    {
        private readonly IRepositorioSerapProva repositorioSerapProva;
        public VerificarSeProvaEhFormatoTAIQueryHandler(IRepositorioSerapProva repositorioSerapProva)
        {
            this.repositorioSerapProva = repositorioSerapProva;
        }

        public async Task<bool> Handle(VerificarSeProvaEhFormatoTAIQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSerapProva.VerificarSeProvaEhFormatoTAI(request.ProvaId);
        }
    }
}
