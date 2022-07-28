using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirTurmaCommand : IRequest<bool>
    {
        public InserirTurmaCommand(TurmaDto turmaDto)
        {
            TurmaDto = turmaDto;
        }

        public TurmaDto TurmaDto { get; set; }
    }
}
