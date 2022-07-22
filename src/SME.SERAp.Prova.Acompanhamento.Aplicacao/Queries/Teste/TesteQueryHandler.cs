using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class TesteQueryHandler : IRequestHandler<TesteQuery, bool>
    {
        public Task<bool> Handle(TesteQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
