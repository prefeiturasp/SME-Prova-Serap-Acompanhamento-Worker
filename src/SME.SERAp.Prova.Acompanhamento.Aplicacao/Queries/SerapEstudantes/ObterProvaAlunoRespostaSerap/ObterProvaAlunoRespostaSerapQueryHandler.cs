using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaAlunoRespostaSerapQueryHandler : IRequestHandler<ObterProvaAlunoRespostaSerapQuery, IEnumerable<ProvaAlunoRespostaDto>>
    {
        private readonly IRepositorioSerapProvaAlunoResposta repositorioSerapProvaAlunoResposta;

        public ObterProvaAlunoRespostaSerapQueryHandler(IRepositorioSerapProvaAlunoResposta repositorioSerapProvaAlunoResposta)
        {
            this.repositorioSerapProvaAlunoResposta = repositorioSerapProvaAlunoResposta ?? throw new ArgumentNullException(nameof(repositorioSerapProvaAlunoResposta));
        }

        public async Task<IEnumerable<ProvaAlunoRespostaDto>> Handle(ObterProvaAlunoRespostaSerapQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSerapProvaAlunoResposta.ObterRespostasAsync(request.ProvaId, request.AlunoRa);
        }
    }
}
