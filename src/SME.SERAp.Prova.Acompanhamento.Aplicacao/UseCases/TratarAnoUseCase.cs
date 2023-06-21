using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarAnoUseCase : AbstractUseCase, ITratarAnoUseCase
    {
        public TratarAnoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var anoDto = mensagemRabbit.ObterObjetoMensagem<AnoDto>();
            if (anoDto == null) return false;

            var ano = await mediator.Send(new ObterAnoPorIdQuery(anoDto.Id.ToString()));
            if (ano == null)
            {
                await mediator.Send(new InserirAnoCommand(anoDto));
            }
            else if (ano.UeId != anoDto.UeId ||
                     ano.AnoLetivo != anoDto.AnoLetivo ||
                     ano.Modalidade != anoDto.Modalidade ||
                     ano.Nome != anoDto.Nome)
            {
                await mediator.Send(new AlterarAnoCommand(anoDto));
            }

            return true;
        }
    }
}
