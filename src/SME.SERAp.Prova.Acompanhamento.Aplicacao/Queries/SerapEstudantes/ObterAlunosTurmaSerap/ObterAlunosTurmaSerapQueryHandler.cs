using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAlunosTurmaSerapQueryHandler : IRequestHandler<ObterAlunosTurmaSerapQuery, IEnumerable<AlunoDto>>
    {
        private readonly IRepositorioSerapTurma repositorioSerapTurma;

        public ObterAlunosTurmaSerapQueryHandler(IRepositorioSerapTurma repositorioSerapTurma)
        {
            this.repositorioSerapTurma = repositorioSerapTurma ?? throw new ArgumentNullException(nameof(repositorioSerapTurma));
        }

        public async Task<IEnumerable<AlunoDto>> Handle(ObterAlunosTurmaSerapQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSerapTurma.ObterAlunosPorIdAsync(request.TurmaId, request.ProvaInicio, request.ProvaFim);
        }
    }
}
