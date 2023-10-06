using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class RemoverTodosOsDadosProvaUseCase : AbstractUseCase, IRemoverTodosOsDadosProvaUseCase
    {
        public RemoverTodosOsDadosProvaUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provaId = long.Parse(mensagemRabbit.Mensagem.ToString());

            await mediator.Send(new ExcluirProvaCommand(provaId.ToString()));
            await mediator.Send(new ExcluirQuestaoProvaPorProvaIdCommand(provaId));
            await mediator.Send(new ExcluirProvaTurmaResultadoPorProvaIdCommand(provaId));
            await mediator.Send(new ExcluirProvaAlunoResultadoPorProvaIdCommand(provaId));
            await mediator.Send(new ExcluirProvaAlunoRespostaPorProvaIdCommand(provaId));

            return true;
        }
    }
}
