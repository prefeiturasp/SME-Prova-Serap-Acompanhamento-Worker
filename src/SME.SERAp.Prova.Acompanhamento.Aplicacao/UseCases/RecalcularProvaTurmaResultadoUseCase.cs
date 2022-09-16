using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System;
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

            await Task.Delay(5000);

            var provaTurmaResultadoBanco = await mediator.Send(new ObterProvaTurmaResultadoQuery(provaTurmaRecalcular.ProvaId, provaTurmaRecalcular.TurmaId));
            if (provaTurmaResultadoBanco == null) return false;

            var provaAlunoResultados = await mediator.Send(new ObterProvaAlunoResultadoPorProvaTurmaQuery(provaTurmaRecalcular.ProvaId, provaTurmaRecalcular.TurmaId));
            if (provaAlunoResultados == null || !provaAlunoResultados.Any()) return false;

            provaTurmaResultadoBanco.TotalAlunos = provaAlunoResultados.Select(pa => pa.AlunoRa).Distinct().Count();
            provaTurmaResultadoBanco.TotalIniciadas = provaAlunoResultados.Where(pa => pa.AlunoInicio != null && pa.AlunoInicio.Value.Date == DateTime.Now.Date && pa.AlunoFim == null).Count();
            provaTurmaResultadoBanco.TotalNaoFinalizados = provaAlunoResultados.Where(pa => pa.AlunoInicio != null && pa.AlunoInicio.Value.Date < DateTime.Now.Date && pa.AlunoFim == null).Count();
            provaTurmaResultadoBanco.TotalFinalizados = provaAlunoResultados.Where(pa => pa.AlunoInicio != null && pa.AlunoFim != null).Count();
            provaTurmaResultadoBanco.QuestoesRespondidas = (long)provaAlunoResultados.Where(pa => pa.AlunoQuestaoRespondida != null && pa.AlunoQuestaoRespondida > 0).Sum(pa => pa.AlunoQuestaoRespondida);
            var tempoTotal = provaAlunoResultados.Where(pa => pa.AlunoFim != null && pa.AlunoTempoMedio > 0).Sum(pa => pa.AlunoTempoMedio);
            provaTurmaResultadoBanco.TempoMedio = CalcularTempoMedioEmMinutos(tempoTotal, provaTurmaResultadoBanco.TotalFinalizados);

            await mediator.Send(new AlterarProvaTurmaResultadoCommand(provaTurmaResultadoBanco));

            return true;
        }

        private long CalcularTempoMedioEmMinutos(int? tempoTotal, long totalFinalizados)
        {
            if (tempoTotal == null || tempoTotal == 0) return 0;
            return ((int)tempoTotal / 60) / totalFinalizados;
        }
    }
}
