using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaAlunoIdResultadoQueryHandler : IRequestHandler<ObterProvaAlunoIdResultadoQuery, ProvaAlunoResultado>
    {
        private readonly IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado;

        public ObterProvaAlunoIdResultadoQueryHandler(IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado)
        {
            this.repositorioProvaAlunoResultado = repositorioProvaAlunoResultado ?? throw new ArgumentNullException(nameof(repositorioProvaAlunoResultado));
        }

        public async Task<ProvaAlunoResultado> Handle(ObterProvaAlunoIdResultadoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProvaAlunoResultado.ObterPorProvaAlunoIdAsync(request.ProvaId, request.AlunoId);
        }
    }
}
