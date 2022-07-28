using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaAlunoResultadoInicioFimUseCase : AbstractUseCase, ITratarProvaAlunoResultadoInicioFimUseCase
    {
        public TratarProvaAlunoResultadoInicioFimUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provaAlunoDto = mensagemRabbit.ObterObjetoMensagem<ProvaAlunoDto>();
            if (provaAlunoDto == null) return false;

            var provaTurmaAlunoSituacao = await mediator.Send(new ObterProvaTurmaAlunoResultadoQuery(provaAlunoDto.ProvaId, provaAlunoDto.AlunoRa));
            if (provaTurmaAlunoSituacao == null) return false;

            var alteracao = false;
            if (!provaTurmaAlunoSituacao.AlunoInicio.HasValue)
            {
                provaTurmaAlunoSituacao.AlunoInicio = provaAlunoDto.CriadoEm;
                alteracao = true;
            }

            if (provaAlunoDto.Status == 2)
            {
                provaTurmaAlunoSituacao.AlunoFim = provaAlunoDto.FinalizadoEm;
                alteracao = true;
            }

            if (alteracao)
                await mediator.Send(new AlterarProvaAlunoResultadoCommand(provaTurmaAlunoSituacao));

            return true;
        }
    }
}
