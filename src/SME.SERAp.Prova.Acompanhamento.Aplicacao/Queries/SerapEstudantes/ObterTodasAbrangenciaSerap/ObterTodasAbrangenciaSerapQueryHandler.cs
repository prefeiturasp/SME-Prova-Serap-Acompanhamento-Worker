using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTodasAbrangenciaSerapQueryHandler : IRequestHandler<ObterTodasAbrangenciaSerapQuery, IEnumerable<AbrangenciaDto>>
    {
        private readonly IRepositorioSerapAbrangencia repositorioSerapAbrangencia;

        public ObterTodasAbrangenciaSerapQueryHandler(IRepositorioSerapAbrangencia repositorioSerapAbrangencia)
        {
            this.repositorioSerapAbrangencia = repositorioSerapAbrangencia ?? throw new ArgumentNullException(nameof(repositorioSerapAbrangencia));
        }

        public async Task<IEnumerable<AbrangenciaDto>> Handle(ObterTodasAbrangenciaSerapQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSerapAbrangencia.ObterPorCoressoIdAsync(request.CoressoId);
        }
    }
}
