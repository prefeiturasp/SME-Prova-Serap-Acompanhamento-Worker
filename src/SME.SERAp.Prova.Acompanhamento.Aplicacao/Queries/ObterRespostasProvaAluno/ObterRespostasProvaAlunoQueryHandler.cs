using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterRespostasProvaAlunoQueryHandler : IRequestHandler<ObterRespostasProvaAlunoQuery, IEnumerable<ProvaAlunoResposta>>
    {
        private readonly IRepositorioProvaAlunoResposta repositorioProvaAlunoResposta;

        public ObterRespostasProvaAlunoQueryHandler(IRepositorioProvaAlunoResposta repositorioProvaAlunoResposta)
        {
            this.repositorioProvaAlunoResposta = repositorioProvaAlunoResposta ?? throw new ArgumentNullException(nameof(repositorioProvaAlunoResposta));
        }

        public async Task<IEnumerable<ProvaAlunoResposta>> Handle(ObterRespostasProvaAlunoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProvaAlunoResposta.ObterProvaAlunoAsync(request.ProvaId, request.AlunoRa);
        }
    }
}
