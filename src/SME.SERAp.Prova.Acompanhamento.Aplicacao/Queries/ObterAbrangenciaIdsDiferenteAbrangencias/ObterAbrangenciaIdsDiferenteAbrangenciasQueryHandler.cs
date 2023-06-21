using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaIdsDiferenteAbrangenciasQueryHandler : IRequestHandler<ObterAbrangenciaIdsDiferenteAbrangenciasQuery, IEnumerable<string>>
    {
        private readonly IRepositorioAbrangencia repositorioAbrangencia;

        public ObterAbrangenciaIdsDiferenteAbrangenciasQueryHandler(IRepositorioAbrangencia repositorioAbrangencia)
        {
            this.repositorioAbrangencia = repositorioAbrangencia ?? throw new ArgumentNullException(nameof(repositorioAbrangencia));
        }

        public async Task<IEnumerable<string>> Handle(ObterAbrangenciaIdsDiferenteAbrangenciasQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAbrangencia.ObterIdsPorGrupoUsuarioDiretenteAbrangenciaIds(request.GrupoId, request.UsuarioId, request.Abrangencias);
        }
    }
}
