using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarDreCommand : IRequest<bool>
    {
        public AlterarDreCommand(DreDto dreDto)
        {
            DreDto = dreDto;
        }

        public DreDto DreDto { get; set; }
    }
}
