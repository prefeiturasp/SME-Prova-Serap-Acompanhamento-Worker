using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirDreCommand : IRequest<bool>
    {
        public InserirDreCommand(DreDto dreDto)
        {
            DreDto = dreDto;
        }

        public DreDto DreDto { get; set; }
    }
}
