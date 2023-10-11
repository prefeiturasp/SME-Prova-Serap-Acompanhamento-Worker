using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaTurmaResultadoPaginadoQueryHandler : IRequestHandler<ObterProvaTurmaResultadoPaginadoQuery, RetornoPaginadoDto<ProvaTurmaResultado>>
    {
        private readonly IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado;

        public ObterProvaTurmaResultadoPaginadoQueryHandler(IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado)
        {
            this.repositorioProvaTurmaResultado = repositorioProvaTurmaResultado ?? throw new ArgumentNullException(nameof(repositorioProvaTurmaResultado));
        }

        public async Task<RetornoPaginadoDto<ProvaTurmaResultado>> Handle(ObterProvaTurmaResultadoPaginadoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProvaTurmaResultado.ObterPaginadoAsync(request.ProvaId, request.ScrollId);
        }
    }
}
