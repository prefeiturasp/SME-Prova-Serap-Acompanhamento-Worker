using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTurmaPorIdQueryHandler : IRequestHandler<ObterTurmaPorIdQuery, Turma>
    {
        private readonly IRepositorioTurma repositorioTurma;

        public ObterTurmaPorIdQueryHandler(IRepositorioTurma repositorioTurma)
        {
            this.repositorioTurma = repositorioTurma ?? throw new ArgumentNullException(nameof(repositorioTurma));
        }

        public Task<Turma> Handle(ObterTurmaPorIdQuery request, CancellationToken cancellationToken)
        {
            return repositorioTurma.ObterPorIdAsync(request.Id);
        }
    }
}
