using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirProvaAlunoQuestaoRespostaCommand : IRequest<bool>
    {
        public InserirProvaAlunoQuestaoRespostaCommand(ProvaAlunoRespostaDto provaAlunoQuestaoRespostaDto)
        {
            ProvaAlunoQuestaoRespostaDto = provaAlunoQuestaoRespostaDto;
        }

        public ProvaAlunoRespostaDto ProvaAlunoQuestaoRespostaDto { get; set; }
    }
}
