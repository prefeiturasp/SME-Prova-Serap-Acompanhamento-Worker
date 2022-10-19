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
    public class ObterGruposCoressoQueryHandler : IRequestHandler<ObterGruposCoressoQuery, IEnumerable<GrupoCoressoDto>>
    {
        private readonly CoressoOptions coressoOptions;
        private readonly IRepositorioCoressoGrupo repositorioCoressoGrupo;

        public ObterGruposCoressoQueryHandler(CoressoOptions coressoOptions, IRepositorioCoressoGrupo repositorioCoressoGrupo)
        {
            this.coressoOptions = coressoOptions ?? throw new ArgumentNullException(nameof(coressoOptions));
            this.repositorioCoressoGrupo = repositorioCoressoGrupo ?? throw new ArgumentNullException(nameof(repositorioCoressoGrupo));
        }

        public async Task<IEnumerable<GrupoCoressoDto>> Handle(ObterGruposCoressoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioCoressoGrupo.ObterGruposPorSistemaModulo(coressoOptions.SistemaId, coressoOptions.AcompanhamentoModuloId);
        }
    }
}
