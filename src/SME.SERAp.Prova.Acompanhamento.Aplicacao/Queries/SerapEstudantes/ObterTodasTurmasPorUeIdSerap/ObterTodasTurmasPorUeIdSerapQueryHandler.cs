using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTodasTurmasPorUeIdSerapQueryHandler : IRequestHandler<ObterTodasTurmasPorUeIdSerapQuery, IEnumerable<TurmaDto>>
    {
        private readonly IRepositorioSerapTurma repositorioSerapTurma;

        public ObterTodasTurmasPorUeIdSerapQueryHandler(IRepositorioSerapTurma repositorioSerapTurma)
        {
            this.repositorioSerapTurma = repositorioSerapTurma ?? throw new ArgumentNullException(nameof(repositorioSerapTurma));
        }

        public async Task<IEnumerable<TurmaDto>> Handle(ObterTodasTurmasPorUeIdSerapQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSerapTurma.ObterPorUeIdAsync(request.UeId);
        }
    }
}
