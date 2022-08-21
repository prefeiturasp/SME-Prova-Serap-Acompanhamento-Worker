using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirProvaQuestaoCommand : IRequest<bool>
    {
        public ExcluirProvaQuestaoCommand(ProvaQuestao provaQuestao)
        {
            ProvaQuestao = provaQuestao;
        }

        public ProvaQuestao ProvaQuestao { get; set; }

    }
}
