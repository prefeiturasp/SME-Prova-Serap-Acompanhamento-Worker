using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    internal class AlterarUeCommand : IRequest<bool>
    {
        public AlterarUeCommand(UeDto ueDto)
        {
            UeDto = ueDto;
        }

        public UeDto UeDto { get; set; }
    }
}
