using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarDreUseCase : AbstractUseCase, ITratarDreUseCase
    {
        public TratarDreUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var dreDto = mensagemRabbit.ObterObjetoMensagem<DreDto>();
            if (dreDto == null) return false;

            var dre = await mediator.Send(new ObterDrePorIdQuery(dreDto.Id));
            if (dre == null)
            {
                await mediator.Send(new InserirDreCommand(dreDto));
            }
            else if (dre.Codigo != dreDto.Codigo ||
                     dre.Abreviacao != dreDto.Abreviacao ||
                     dre.Nome != dreDto.Nome)
            {
                await mediator.Send(new AlterarDreCommand(dreDto));
            }

            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.UeSync, dreDto.Id));

            return true;
        }
    }
}
