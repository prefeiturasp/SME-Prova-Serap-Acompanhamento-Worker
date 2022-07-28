using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirAnoCommand : IRequest<bool>
    {
        public InserirAnoCommand(AnoDto anoDto)
        {
            AnoDto = anoDto;
        }

        public AnoDto AnoDto { get; set; }
    }
}
