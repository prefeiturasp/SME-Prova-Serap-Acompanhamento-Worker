using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarTurmaCommand : IRequest<bool>
    {
        public AlterarTurmaCommand(TurmaDto turmaDto)
        {
            TurmaDto = turmaDto;
        }

        public TurmaDto TurmaDto { get; set; }
    }
}
