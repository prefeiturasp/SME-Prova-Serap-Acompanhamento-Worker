using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
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

            var provaTurmaResultadoBanco = await mediator.Send(new ObterProvaTurmaResultadoQuery(provaTurmaRecalcular.ProvaId, provaTurmaRecalcular.TurmaId));
            if (provaTurmaResultadoBanco == null) return false;

            var provaAlunoResultados = await mediator.Send(new ObterProvaAlunoResultadoPorProvaTurmaQuery(provaTurmaRecalcular.ProvaId, provaTurmaRecalcular.TurmaId));
            var tempoTotal = provaAlunoResultados.Where(pa => pa.AlunoFim != null && pa.AlunoTempoMedio > 0).Sum(pa => pa.AlunoTempoMedio);
            int totalQuestoesRespondidas = 0;

            provaTurmaResultadoBanco.TotalAlunos = provaAlunoResultados.Select(pa => pa.AlunoRa).Distinct().Count();

            return true;
        }
    }
}
