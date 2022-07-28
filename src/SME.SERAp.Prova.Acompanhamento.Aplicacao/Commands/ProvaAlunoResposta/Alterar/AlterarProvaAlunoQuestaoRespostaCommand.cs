using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarProvaAlunoQuestaoRespostaCommand : IRequest<bool>
    {
        public AlterarProvaAlunoQuestaoRespostaCommand(ProvaAlunoRespostaDto provaAlunoQuestaoRespostaDto)
        {
            ProvaAlunoQuestaoRespostaDto = provaAlunoQuestaoRespostaDto;
        }

        public ProvaAlunoRespostaDto ProvaAlunoQuestaoRespostaDto { get; set; }
    }
}
