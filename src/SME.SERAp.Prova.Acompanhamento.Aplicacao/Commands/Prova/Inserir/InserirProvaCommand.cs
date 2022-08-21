using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirProvaCommand : IRequest<bool>
    {
        public InserirProvaCommand(ProvaDto provaDto)
        {
            ProvaDto = provaDto;
        }

        public ProvaDto ProvaDto { get; set; }
    }
}
