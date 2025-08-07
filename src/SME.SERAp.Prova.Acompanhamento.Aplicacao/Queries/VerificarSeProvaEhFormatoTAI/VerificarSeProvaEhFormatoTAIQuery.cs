using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries
{
    public class VerificarSeProvaEhFormatoTAIQuery : IRequest<bool>
    {
        public long ProvaId { get; set; }
        public VerificarSeProvaEhFormatoTAIQuery(long provaId)
        {
            ProvaId = provaId;
        }
    }
}
