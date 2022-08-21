using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarTurmaUseCase : AbstractUseCase, ITratarTurmaUseCase
    {
        public TratarTurmaUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var turmaDto = mensagemRabbit.ObterObjetoMensagem<TurmaDto>();
            if (turmaDto == null) return false;

            var turma = await mediator.Send(new ObterTurmaPorIdQuery(turmaDto.Id));
            if (turma == null)
            {
                await mediator.Send(new InserirTurmaCommand(turmaDto));
            }
            else if (turma.Codigo != turmaDto.Codigo ||
                turma.AnoLetivo != turmaDto.AnoLetivo ||
                turma.Ano != turmaDto.Ano ||
                turma.Nome != turmaDto.Nome ||
                turma.Modalidade != turmaDto.Modalidade ||
                turma.EtapaEja != turmaDto.EtapaEja ||
                turma.Turno != turmaDto.Turno ||
                turma.SerieEnsino != turmaDto.SerieEnsino)
            {
                await mediator.Send(new AlterarTurmaCommand(turmaDto));
            }

            return true;
        }
    }
}
