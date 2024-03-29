﻿using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ConsolidarProvaAlunoRespostaUseCase : AbstractUseCase, IConsolidarProvaAlunoRespostaUseCase
    {
        public ConsolidarProvaAlunoRespostaUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provaAluno = mensagemRabbit.ObterObjetoMensagem<ProvaAlunoDto>();
            if (provaAluno == null) return false;

            // aguarda a indexação dos dados para recalcular.
            //await Task.Delay(5000);

            var respostas = await mediator.Send(new ObterRespostasProvaAlunoQuery(provaAluno.ProvaId, provaAluno.AlunoRa));
            if (respostas != null && respostas.Any())
            {
                var provaAlunoResultados = await mediator.Send(new ObterProvaAlunoResultadoQuery(provaAluno.ProvaId, provaAluno.AlunoRa));
                if (provaAlunoResultados == null || !provaAlunoResultados.Any()) return false;

                int alunoQuestaoRespondida = respostas.Count(t => t.AlternativaId.HasValue);
                int alunoTempo = respostas.Sum(s => s.Tempo.GetValueOrDefault());

                foreach (var resultado in provaAlunoResultados)
                {
                    if (resultado.AlunoQuestaoRespondida != alunoQuestaoRespondida ||
                        resultado.AlunoTempo != alunoTempo)
                    {
                        resultado.AlunoQuestaoRespondida = alunoQuestaoRespondida;
                        resultado.AlunoTempo = alunoTempo;

                        await mediator.Send(new AlterarProvaAlunoResultadoCommand(resultado));
                    }
                }

                var provaAlunoResultado = provaAlunoResultados.FirstOrDefault();
                var provaTurmaRecalcular = new ProvaTurmaRecalcularDto(provaAlunoResultado.ProvaId, provaAlunoResultado.TurmaId);
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaTurmaResultadoRecalcular, provaTurmaRecalcular));
            }

            return true;
        }
    }
}
