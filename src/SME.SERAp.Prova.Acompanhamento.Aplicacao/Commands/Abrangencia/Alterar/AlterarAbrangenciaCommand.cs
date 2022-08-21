using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarAbrangenciaCommand : IRequest<bool>
    {
        public AlterarAbrangenciaCommand(AbrangenciaDto abrangenciaDto)
        {
            AbrangenciaDto = abrangenciaDto;
        }

        public AbrangenciaDto AbrangenciaDto { get; set; }
    }
}
