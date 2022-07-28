using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries.ObterProvaAlunoQuestaoRespostaPorProvaAlunoQuestaoId
{
    public class ObterProvaAlunoRespostaQueryHandler : IRequestHandler<ObterProvaAlunoRespostaQuery, ProvaAlunoResposta>
    {
        private readonly IRepositorioProvaAlunoResposta repositorioProvaAlunoResposta;

        public ObterProvaAlunoRespostaQueryHandler(IRepositorioProvaAlunoResposta repositorioProvaAlunoResposta)
        {
            this.repositorioProvaAlunoResposta = repositorioProvaAlunoResposta ?? throw new ArgumentNullException(nameof(repositorioProvaAlunoResposta));
        }

        public async Task<ProvaAlunoResposta> Handle(ObterProvaAlunoRespostaQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProvaAlunoResposta.ObterPorProvaAlunoQuestaoAsync(request.ProvaId, request.AlunoRa, request.QuestaoId);
        }
    }
}
