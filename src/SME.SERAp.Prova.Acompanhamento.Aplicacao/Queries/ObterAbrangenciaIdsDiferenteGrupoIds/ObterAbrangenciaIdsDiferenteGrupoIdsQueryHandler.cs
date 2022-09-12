using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaIdsDiferenteGrupoIdsQueryHandler : IRequestHandler<ObterAbrangenciaIdsDiferenteGrupoIdsQuery, IEnumerable<string>>
    {
        private readonly IRepositorioAbrangencia repositorioAbrangencia;

        public ObterAbrangenciaIdsDiferenteGrupoIdsQueryHandler(IRepositorioAbrangencia repositorioAbrangencia)
        {
            this.repositorioAbrangencia = repositorioAbrangencia ?? throw new ArgumentNullException(nameof(repositorioAbrangencia));
        }

        public async Task<IEnumerable<string>> Handle(ObterAbrangenciaIdsDiferenteGrupoIdsQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAbrangencia.ObterIdsDiretenteGrupoIds(request.GrupoIds);
        }
    }
}
