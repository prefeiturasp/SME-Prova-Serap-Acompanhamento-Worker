using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaTurmaResultadoQueryHandler : IRequestHandler<ObterProvaTurmaResultadoQuery, ProvaTurmaResultado>
    {
        private readonly IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado;

        public ObterProvaTurmaResultadoQueryHandler(IRepositorioProvaTurmaResultado repositorioProvaTurmaResultado)
        {
            this.repositorioProvaTurmaResultado = repositorioProvaTurmaResultado ?? throw new ArgumentNullException(nameof(repositorioProvaTurmaResultado));
        }

        public async Task<ProvaTurmaResultado> Handle(ObterProvaTurmaResultadoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProvaTurmaResultado.ObterPorProvaTurmaAsync(request.ProvaId, request.TurmaId);
        }
    }
}
