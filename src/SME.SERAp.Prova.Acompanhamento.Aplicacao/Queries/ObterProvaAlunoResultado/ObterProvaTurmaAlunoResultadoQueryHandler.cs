using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaTurmaAlunoResultadoQueryHandler : IRequestHandler<ObterProvaTurmaAlunoResultadoQuery, ProvaAlunoResultado>
    {
        private readonly IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado;

        public ObterProvaTurmaAlunoResultadoQueryHandler(IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado)
        {
            this.repositorioProvaAlunoResultado = repositorioProvaAlunoResultado ?? throw new ArgumentNullException(nameof(repositorioProvaAlunoResultado));
        }

        public async Task<ProvaAlunoResultado> Handle(ObterProvaTurmaAlunoResultadoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProvaAlunoResultado.ObterPorProvaAlunoRaAsync(request.ProvaId, request.AlunoRa);
        }
    }
}
