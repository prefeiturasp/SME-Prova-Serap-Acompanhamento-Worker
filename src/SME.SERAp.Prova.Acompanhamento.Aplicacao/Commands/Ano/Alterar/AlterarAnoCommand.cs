using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarAnoCommand : IRequest<bool>
    {
        public AlterarAnoCommand(AnoDto anoDto)
        {
            AnoDto = anoDto;
        }

        public AnoDto AnoDto { get; set; }
    }
}
