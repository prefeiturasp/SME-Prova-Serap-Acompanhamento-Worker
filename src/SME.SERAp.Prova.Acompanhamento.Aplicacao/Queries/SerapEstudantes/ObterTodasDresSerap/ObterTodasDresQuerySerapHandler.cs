using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTodasDresQuerySerapHandler : IRequestHandler<ObterTodasDresQuerySerap, IEnumerable<DreDto>>
    {
        private readonly IRepositorioSerapDre repositorioSerapDre;

        public ObterTodasDresQuerySerapHandler(IRepositorioSerapDre repositorioSerapDre)
        {
            this.repositorioSerapDre = repositorioSerapDre ?? throw new ArgumentNullException(nameof(repositorioSerapDre));
        }

        public async Task<IEnumerable<DreDto>> Handle(ObterTodasDresQuerySerap request, CancellationToken cancellationToken)
        {
            return await repositorioSerapDre.ObterTodosAsync();
        }
    }
}
