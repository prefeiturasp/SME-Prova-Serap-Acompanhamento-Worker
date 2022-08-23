using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirProvaTurmaResultadoCommandHandler : IRequestHandler<ExcluirProvaTurmaResultadoCommand, bool>
    {
        private readonly IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado;

        public ExcluirProvaTurmaResultadoCommandHandler(IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado)
        {
            this.repositorioProvaTurmaResultado = repositorioProvaTurmaResultado;
        }

        public async Task<bool> Handle(ExcluirProvaTurmaResultadoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioProvaTurmaResultado.ExcluirPorProvaTurmaIdAsync(request.ProvaTurmaResultado);
        }
    }
}
