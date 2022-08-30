using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
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

            var provaAlunoResultados = await mediator.Send(new ObterProvaAlunoResultadoQuery(provaAlunoDto.ProvaId, provaAlunoDto.AlunoRa));
            if (provaAlunoResultados == null || !provaAlunoResultados.Any()) return false;

            foreach (var resultado in provaAlunoResultados)
            {
                var alteracao = false;
                if (!resultado.AlunoInicio.HasValue)
                {
                    resultado.AlunoInicio = provaAlunoDto.CriadoEm;
                    alteracao = true;
                }

                if (provaAlunoDto.Status == 2)
                {
                    resultado.AlunoFim = provaAlunoDto.FinalizadoEm;
                    alteracao = true;
                }

                if (alteracao)
                    await mediator.Send(new AlterarProvaAlunoResultadoCommand(resultado));
            }

            var provaAlunoResultado = provaAlunoResultados.FirstOrDefault();
            var provaTurmaRecalcular = new ProvaTurmaRecalcularDto(provaAlunoResultado.ProvaId, provaAlunoResultado.TurmaId);
            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaTurmaResultadoRecalcular, provaTurmaRecalcular));

            return true;
        }
    }
}
