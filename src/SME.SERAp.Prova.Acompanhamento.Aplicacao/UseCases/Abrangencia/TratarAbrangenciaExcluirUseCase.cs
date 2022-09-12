using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarAbrangenciaExcluirUseCase : AbstractUseCase, ITratarAbrangenciaExcluirUseCase
    {
        public TratarAbrangenciaExcluirUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var grupoIds = mensagemRabbit.ObterObjetoMensagem<List<Guid>>();
            if (grupoIds == null || !grupoIds.Any()) return false;

            var abrangenciaIdsExcluir = await mediator.Send(new ObterAbrangenciaIdsDiferenteGrupoIdsQuery(grupoIds.ToArray()));
            if (abrangenciaIdsExcluir != null && abrangenciaIdsExcluir.Any())
            {
                foreach (var abrangenciaId in abrangenciaIdsExcluir)
                {
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.AbrangenciaExcluir, abrangenciaId));
                }
            }

            return true;
        }
    }
}
