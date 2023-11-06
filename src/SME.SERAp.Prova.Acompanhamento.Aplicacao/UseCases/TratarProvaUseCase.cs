using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaUseCase : AbstractUseCase, ITratarProvaUseCase
    {
        public TratarProvaUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provaDto = mensagemRabbit.ObterObjetoMensagem<ProvaDto>();
            if (provaDto == null) return false;

            var prova = await mediator.Send(new ObterProvaPorIdQuery(provaDto.Id.ToString()));
            if (prova == null)
            {
                await mediator.Send(new InserirProvaCommand(provaDto));
            }
            else if (prova.Codigo != provaDto.Codigo ||
                     prova.Descricao != provaDto.Descricao ||
                     prova.Modalidade != provaDto.Modalidade ||
                     prova.Inicio != provaDto.Inicio ||
                     prova.Fim != provaDto.Fim)
            {
                await mediator.Send(new AlterarProvaCommand(provaDto));
            }

            return true;
        }
    }
}
