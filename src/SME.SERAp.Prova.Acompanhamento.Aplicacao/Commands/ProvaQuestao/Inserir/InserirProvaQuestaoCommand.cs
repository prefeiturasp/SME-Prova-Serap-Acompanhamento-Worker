using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirProvaQuestaoCommand : IRequest<bool>
    {
        public InserirProvaQuestaoCommand(ProvaQuestao provaQuestao)
        {
            ProvaQuestao = provaQuestao;
        }

        public ProvaQuestao ProvaQuestao { get; set; }

    }
}
