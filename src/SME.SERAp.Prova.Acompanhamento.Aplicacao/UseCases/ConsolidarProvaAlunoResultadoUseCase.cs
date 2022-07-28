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

            var respostas = await mediator.Send(new ObterRespostasProvaAlunoQuery(provaAluno.ProvaId, provaAluno.AlunoRa));
            if (respostas != null && respostas.Any())
            {
                var provaAlunoResultado = await mediator.Send(new ObterProvaTurmaAlunoResultadoQuery(provaAluno.ProvaId, provaAluno.AlunoRa));
                if (provaAlunoResultado == null) return false;

                provaAlunoResultado.AlunoQuestaoRespondida = respostas.Count(t => t.AlternativaId.HasValue);
                provaAlunoResultado.AlunoTempoMedio = respostas.Sum(s => s.Tempo) / provaAlunoResultado.AlunoQuestaoRespondida;

                await mediator.Send(new AlterarProvaAlunoResultadoCommand(provaAlunoResultado));
            }

            return true;
        }
    }
}
