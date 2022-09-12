using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.Coresso;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaCoressoPorUsuarioGrupoQueryHandler : IRequestHandler<ObterAbrangenciaCoressoPorUsuarioGrupoQuery, IEnumerable<string>>
    {
        private readonly IRepositorioCoressoAbrangencia repositorioCoressoAbrangencia;

        public ObterAbrangenciaCoressoPorUsuarioGrupoQueryHandler(IRepositorioCoressoAbrangencia repositorioCoressoAbrangencia)
        {
            this.repositorioCoressoAbrangencia = repositorioCoressoAbrangencia ?? throw new ArgumentNullException(nameof(repositorioCoressoAbrangencia));
        }

        public async Task<IEnumerable<string>> Handle(ObterAbrangenciaCoressoPorUsuarioGrupoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioCoressoAbrangencia.ObterAbrangenciaPorGrupoUsuario(request.GrupoId, request.UsuarioId);
        }
    }
}
