using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaIdsPorGrupoDiferenteUsuarioIdsQueryHandler : IRequestHandler<ObterAbrangenciaIdsPorGrupoDiferenteUsuarioIdsQuery, IEnumerable<string>>
    {
        private readonly IRepositorioAbrangencia repositorioAbrangencia;

        public ObterAbrangenciaIdsPorGrupoDiferenteUsuarioIdsQueryHandler(IRepositorioAbrangencia repositorioAbrangencia)
        {
            this.repositorioAbrangencia = repositorioAbrangencia ?? throw new ArgumentNullException(nameof(repositorioAbrangencia));
        }

        public async Task<IEnumerable<string>> Handle(ObterAbrangenciaIdsPorGrupoDiferenteUsuarioIdsQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAbrangencia.ObterIdsPorGrupoDiretenteUsuarioIds(request.GrupoId, request.UsuarioIds);
        }
    }
}
