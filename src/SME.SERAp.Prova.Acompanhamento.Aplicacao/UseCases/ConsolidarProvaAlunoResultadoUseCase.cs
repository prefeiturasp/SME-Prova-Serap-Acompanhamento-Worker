using MediatR;
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

            await Task.Delay(5000);

            var respostas = await mediator.Send(new ObterRespostasProvaAlunoQuery(provaAluno.ProvaId, provaAluno.AlunoRa));
            if (respostas != null && respostas.Any())
            {
                var provaAlunoResultados = await mediator.Send(new ObterProvaAlunoResultadoQuery(provaAluno.ProvaId, provaAluno.AlunoRa));
                if (provaAlunoResultados == null || !provaAlunoResultados.Any()) return false;

                foreach (var resultado in provaAlunoResultados)
                {
                    resultado.AlunoQuestaoRespondida = respostas.Count(t => t.AlternativaId.HasValue);
                    resultado.AlunoTempoMedio = respostas.Sum(s => s.Tempo) / resultado.AlunoQuestaoRespondida;

                    await mediator.Send(new AlterarProvaAlunoResultadoCommand(resultado));
                }

                var provaAlunoResultado = provaAlunoResultados.FirstOrDefault();
                var provaTurmaRecalcular = new ProvaTurmaRecalcularDto(provaAlunoResultado.ProvaId, provaAlunoResultado.TurmaId);
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaTurmaResultadoRecalcular, provaTurmaRecalcular));
            }            

            return true;
        }
    }
}
