using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirAbrangenciaCommand : IRequest<bool>
    {
        public InserirAbrangenciaCommand(AbrangenciaDto abrangenciaDto)
        {
            AbrangenciaDto = abrangenciaDto;
        }

        public AbrangenciaDto AbrangenciaDto { get; set; }
    }
}
