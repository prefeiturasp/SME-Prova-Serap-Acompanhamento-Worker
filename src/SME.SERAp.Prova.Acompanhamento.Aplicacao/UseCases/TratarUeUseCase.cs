using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarUeUseCase : AbstractUseCase, ITratarUeUseCase
    {
        public TratarUeUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var ueDto = mensagemRabbit.ObterObjetoMensagem<UeDto>();
            if (ueDto == null) return false;

            var eu = await mediator.Send(new ObterUePorIdQuery(ueDto.Id));
            if (eu == null)
            {
                await mediator.Send(new InserirUeCommand(ueDto));
            }
            else if (eu.Codigo != ueDto.Codigo ||
                     eu.Nome != ueDto.Nome)
            {
                await mediator.Send(new AlterarUeCommand(ueDto));
            }

            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.TurmaSync, ueDto.Id));

            return true;
        }
    }
}
