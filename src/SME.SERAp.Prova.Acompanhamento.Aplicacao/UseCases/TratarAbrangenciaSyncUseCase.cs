using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarAbrangenciaSyncUseCase : AbstractUseCase, ITratarAbrangenciaSyncUseCase
    {
        public TratarAbrangenciaSyncUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var grupos = await mediator.Send(new ObterTodosGruposUsuarioSerapQuery());

            if (grupos != null && grupos.Any())
            {
                foreach (var grupo in grupos)
                {
                    var agrangencias = await mediator.Send(new ObterTodasAbrangenciaSerapQuery(grupo));

                    if (agrangencias != null && agrangencias.Any())
                    {
                        foreach (var agrangencia in agrangencias)
                        {
                            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.AbrangenciaTratar, agrangencia));
                        }
                    }
                }
            }

            return true;
        }
    }
}
