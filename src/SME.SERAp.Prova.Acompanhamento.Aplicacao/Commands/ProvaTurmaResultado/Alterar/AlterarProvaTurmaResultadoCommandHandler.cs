using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarProvaTurmaResultadoCommandHandler : IRequestHandler<AlterarProvaTurmaResultadoCommand, bool>
    {
        private readonly IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado;

        public AlterarProvaTurmaResultadoCommandHandler(IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado)
        {
            this.repositorioProvaTurmaResultado = repositorioProvaTurmaResultado;
        }

        public async Task<bool> Handle(AlterarProvaTurmaResultadoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioProvaTurmaResultado.AlterarAsync(request.ProvaTurmaResultado);
        }
    }
}
