using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class TesteUseCase : AbstractUseCase, ITesteUseCase
    {
        public TesteUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var testeQuey = await mediator.Send(new TesteQuery());
            var testeCommand = await mediator.Send(new InserirTesteCommand(mensagemRabbit.Mensagem.ToString()));
            return testeQuey || testeCommand;
        }
    }
}
