using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaAlunoResultadoPaginadoQueryHandler : IRequestHandler<ObterProvaAlunoResultadoPaginadoQuery, RetornoPaginadoDto<ProvaAlunoResultado>>
    {
        private readonly IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado;

        public ObterProvaAlunoResultadoPaginadoQueryHandler(IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado)
        {
            this.repositorioProvaAlunoResultado = repositorioProvaAlunoResultado ?? throw new ArgumentNullException(nameof(repositorioProvaAlunoResultado));
        }

        public async Task<RetornoPaginadoDto<ProvaAlunoResultado>> Handle(ObterProvaAlunoResultadoPaginadoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProvaAlunoResultado.ObterPaginadoAsync(request.ProvaId, request.TurmaId, request.ScrollId);
        }
    }
}
