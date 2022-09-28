using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.Coresso;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterUsuariosCoressoPorGrupoQueryHandler : IRequestHandler<ObterUsuariosCoressoPorGrupoQuery, IEnumerable<UsuarioCoressoDto>>
    {
        private readonly CoressoOptions coressoOptions;
        private readonly IRepositorioCoressoUsuario repositorioCoressoUsuario;

        public ObterUsuariosCoressoPorGrupoQueryHandler(CoressoOptions coressoOptions, IRepositorioCoressoUsuario repositorioCoressoUsuario)
        {
            this.coressoOptions = coressoOptions ?? throw new ArgumentNullException(nameof(coressoOptions));
            this.repositorioCoressoUsuario = repositorioCoressoUsuario ?? throw new ArgumentNullException(nameof(repositorioCoressoUsuario));
        }

        public async Task<IEnumerable<UsuarioCoressoDto>> Handle(ObterUsuariosCoressoPorGrupoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioCoressoUsuario.ObterUsuariosPorSistemaGrupo(coressoOptions.SistemaId, request.GrupoId);
        }
    }
}
