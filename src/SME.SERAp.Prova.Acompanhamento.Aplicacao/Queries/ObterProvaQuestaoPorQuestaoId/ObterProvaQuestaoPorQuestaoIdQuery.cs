using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaQuestaoPorQuestaoIdQuery : IRequest<ProvaQuestao>
    {
        public ObterProvaQuestaoPorQuestaoIdQuery(long questaoId)
        {
            QuestaoId = questaoId;
        }

        public long QuestaoId { get; set; }

    }
}
