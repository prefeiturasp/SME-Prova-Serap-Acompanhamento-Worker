﻿using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaAlunoSyncUseCase : AbstractUseCase, ITratarProvaAlunoSyncUseCase
    {
        public TratarProvaAlunoSyncUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provas = await mediator.Send(new ObterProvasEmAndamentoQuery());
            if (provas != null && provas.Any())
            {
                foreach (var prova in provas)
                {
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaAlunoTurmaSync, prova.Id));
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaAlunoAdesao, prova.Id));
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaTurmaAdesao, prova.Id));
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaQuestaoSync, prova.Id));
                }
            }

            return true;
        }
    }
}
