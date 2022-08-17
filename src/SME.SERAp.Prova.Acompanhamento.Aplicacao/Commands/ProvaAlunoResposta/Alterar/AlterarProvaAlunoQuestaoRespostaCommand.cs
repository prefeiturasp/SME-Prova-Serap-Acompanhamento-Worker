using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarProvaAlunoQuestaoRespostaCommand : IRequest<bool>
    {
        public AlterarProvaAlunoQuestaoRespostaCommand(string idIndex, ProvaAlunoRespostaDto provaAlunoQuestaoRespostaDto)
        {
            IdIndex = idIndex;
            ProvaAlunoQuestaoRespostaDto = provaAlunoQuestaoRespostaDto;
        }

        public string IdIndex { get; set; }
        public ProvaAlunoRespostaDto ProvaAlunoQuestaoRespostaDto { get; set; }
    }
}
