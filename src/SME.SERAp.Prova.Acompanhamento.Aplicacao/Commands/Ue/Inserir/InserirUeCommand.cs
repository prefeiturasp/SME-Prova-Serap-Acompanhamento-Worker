using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    internal class InserirUeCommand : IRequest<bool>
    {
        public InserirUeCommand(UeDto ueDto)
        {
            UeDto = ueDto;
        }

        public UeDto UeDto { get; set; }
    }
}
