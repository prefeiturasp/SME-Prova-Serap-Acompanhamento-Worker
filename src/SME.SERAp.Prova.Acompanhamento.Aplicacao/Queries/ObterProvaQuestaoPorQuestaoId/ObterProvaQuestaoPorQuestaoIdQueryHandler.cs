using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaQuestaoPorQuestaoIdQueryHandler : IRequestHandler<ObterProvaQuestaoPorQuestaoIdQuery, ProvaQuestao>
    {

        private readonly IRepositorioProvaQuestao repositorioProvaQuestao;

        public ObterProvaQuestaoPorQuestaoIdQueryHandler(IRepositorioProvaQuestao repositorioProvaQuestao)
        {
            this.repositorioProvaQuestao = repositorioProvaQuestao ?? throw new ArgumentNullException(nameof(repositorioProvaQuestao));
        }

        public async Task<ProvaQuestao> Handle(ObterProvaQuestaoPorQuestaoIdQuery request, CancellationToken cancellationToken)
        {
            await repositorioProvaQuestao.CriarIndexAsync();
            return await repositorioProvaQuestao.ObterPorQuestaoIdAsync(request.QuestaoId);
        }
    }
}
