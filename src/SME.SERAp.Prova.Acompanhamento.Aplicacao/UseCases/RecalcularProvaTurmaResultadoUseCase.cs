using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class RecalcularProvaTurmaResultadoUseCase : AbstractUseCase, IRecalcularProvaTurmaResultadoUseCase
    {
        public RecalcularProvaTurmaResultadoUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provaTurmaRecalcular = mensagemRabbit.ObterObjetoMensagem<ProvaTurmaRecalcularDto>();
            if (provaTurmaRecalcular == null) return false;

            // aguarda a indexação dos dados para recalcular.
            //await Task.Delay(5000);

            var provaTurmaResultadoBanco = await mediator.Send(new ObterProvaTurmaResultadoQuery(provaTurmaRecalcular.ProvaId, provaTurmaRecalcular.TurmaId));
            if (provaTurmaResultadoBanco == null) return false;

            var provaAlunoResultados = await mediator.Send(new ObterProvaAlunoResultadoPorProvaTurmaQuery(provaTurmaRecalcular.ProvaId, provaTurmaRecalcular.TurmaId));
            if (provaAlunoResultados == null || !provaAlunoResultados.Any()) return false;

            List<long> alunos = new();
            int totalAlunos = 0;
            int totalIniciadas = 0;
            int totalNaoFinalizados = 0;
            int totalFinalizados = 0;
            int questoesRespondidas = 0;
            int tempoTotal = 0;

            foreach (var provaAlunoResultado in provaAlunoResultados)
            {
                if (provaAlunoResultado.AlunoSituacao != 99 &&
                    !alunos.Contains(provaAlunoResultado.AlunoRa))
                {
                    totalAlunos++;
                    alunos.Add(provaAlunoResultado.AlunoRa);
                }

                if (provaAlunoResultado.AlunoInicio != null &&
                    provaAlunoResultado.AlunoInicio.Value.Date == DateTime.Now.Date &&
                    provaAlunoResultado.AlunoFim == null)
                    totalIniciadas++;

                if (provaAlunoResultado.AlunoInicio != null &&
                    provaAlunoResultado.AlunoInicio.Value.Date < DateTime.Now.Date &&
                    provaAlunoResultado.AlunoFim == null)
                    totalNaoFinalizados++;

                if (provaAlunoResultado.AlunoInicio != null &&
                    provaAlunoResultado.AlunoFim != null)
                    totalFinalizados++;

                if (provaAlunoResultado.AlunoQuestaoRespondida != null &&
                    provaAlunoResultado.AlunoQuestaoRespondida > 0)
                    questoesRespondidas += provaAlunoResultado.AlunoQuestaoRespondida.GetValueOrDefault();

                if (provaAlunoResultado.AlunoFim != null &&
                    provaAlunoResultado.AlunoTempo > 0)
                    tempoTotal += provaAlunoResultado.AlunoTempo.GetValueOrDefault();
            }

            var tempoMedio = CalcularTempoMedioEmMinutos(tempoTotal, totalFinalizados);

            if (provaTurmaResultadoBanco.TotalAlunos != totalAlunos ||
                provaTurmaResultadoBanco.TotalIniciadas != totalIniciadas ||
                provaTurmaResultadoBanco.TotalNaoFinalizados != totalNaoFinalizados ||
                provaTurmaResultadoBanco.TotalFinalizados != totalFinalizados ||
                provaTurmaResultadoBanco.QuestoesRespondidas != questoesRespondidas ||
                provaTurmaResultadoBanco.TempoMedio != tempoMedio
                )
            {
                provaTurmaResultadoBanco.TotalAlunos = totalAlunos;
                provaTurmaResultadoBanco.TotalIniciadas = totalIniciadas;
                provaTurmaResultadoBanco.TotalNaoFinalizados = totalNaoFinalizados;
                provaTurmaResultadoBanco.TotalFinalizados = totalFinalizados;
                provaTurmaResultadoBanco.QuestoesRespondidas = questoesRespondidas;
                provaTurmaResultadoBanco.TempoMedio = tempoMedio;

                await mediator.Send(new AlterarProvaTurmaResultadoCommand(provaTurmaResultadoBanco));
            }

            return true;
        }

        private static long CalcularTempoMedioEmMinutos(int? tempoTotal, long totalFinalizados)
        {
            if (tempoTotal == null || tempoTotal == 0) return 0;
            return ((int)tempoTotal / 60) / totalFinalizados;
        }
    }
}
