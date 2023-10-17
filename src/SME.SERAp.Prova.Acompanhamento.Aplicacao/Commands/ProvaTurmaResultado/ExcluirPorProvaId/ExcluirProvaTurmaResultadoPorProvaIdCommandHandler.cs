using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirProvaTurmaResultadoPorProvaIdCommandHandler : IRequestHandler<ExcluirProvaTurmaResultadoPorProvaIdCommand, bool>
    {
        private readonly IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado;

        public ExcluirProvaTurmaResultadoPorProvaIdCommandHandler(IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado)
        {
            this.repositorioProvaTurmaResultado = repositorioProvaTurmaResultado ?? throw new ArgumentNullException(nameof(repositorioProvaTurmaResultado));
        }

        public async Task<bool> Handle(ExcluirProvaTurmaResultadoPorProvaIdCommand request, CancellationToken cancellationToken)
        {
            return await repositorioProvaTurmaResultado.ExcluirPorProvaIdAsync(request.ProvaId);
        }
    }
}
