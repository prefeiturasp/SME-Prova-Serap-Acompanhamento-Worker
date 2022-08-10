using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Commands.ProvaTurmaResultado.Inserir
{
    public class InserirProvaTurmaResultadoCommandHandler : IRequestHandler<InserirProvaTurmaResultadoCommand, bool>
    {
        private readonly IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado;

        public InserirProvaTurmaResultadoCommandHandler(IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado)
        {
            this.repositorioProvaTurmaResultado = repositorioProvaTurmaResultado ?? throw new ArgumentNullException(nameof(repositorioProvaTurmaResultado));
        }

        public async Task<bool> Handle(InserirProvaTurmaResultadoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioProvaTurmaResultado.InserirAsync(request.ProvaTurmaResultado);
        }
    }
}
