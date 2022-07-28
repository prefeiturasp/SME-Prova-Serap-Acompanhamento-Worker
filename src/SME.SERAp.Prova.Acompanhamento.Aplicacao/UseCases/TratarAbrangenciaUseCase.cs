using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarAbrangenciaUseCase : AbstractUseCase, ITratarAbrangenciaUseCase
    {
        public TratarAbrangenciaUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var abrangenciaDto = mensagemRabbit.ObterObjetoMensagem<AbrangenciaDto>();
            if (abrangenciaDto == null) return false;

            var abrangencia = await mediator.Send(new ObterAbrangenciaPorIdQuery(abrangenciaDto.Id));
            if (abrangencia == null)
            {
                await mediator.Send(new InserirAbrangenciaCommand(abrangenciaDto));
            }
            else if (abrangencia.CoressoId != abrangenciaDto.CoressoId ||
                     abrangencia.Login != abrangenciaDto.Login ||
                     abrangencia.Usuario != abrangenciaDto.Usuario ||
                     abrangencia.Grupo != abrangenciaDto.Grupo ||
                     abrangencia.DreId != abrangenciaDto.DreId ||
                     abrangencia.UeId != abrangenciaDto.UeId ||
                     abrangencia.TurmaId != abrangenciaDto.TurmaId)
            {
                await mediator.Send(new AlterarAbrangenciaCommand(abrangenciaDto));
            }

            return true;
        }
    }
}
