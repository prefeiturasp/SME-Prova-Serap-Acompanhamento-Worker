using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaAlunoResultadoUseCase : AbstractUseCase, ITratarProvaAlunoResultadoUseCase
    {
        public TratarProvaAlunoResultadoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provaTurmaAlunoSituacao = mensagemRabbit.ObterObjetoMensagem<ProvaAlunoResultado>();
            if (provaTurmaAlunoSituacao == null) return false;

            var provaTurmaAlunoSituacaoBanco = await mediator.Send(new ObterProvaTurmaAlunoResultadoQuery(provaTurmaAlunoSituacao.ProvaId, provaTurmaAlunoSituacao.AlunoRa));

            if (provaTurmaAlunoSituacaoBanco == null)
            {
                await mediator.Send(new InserirProvaAlunoResultadoCommand(provaTurmaAlunoSituacao));
            }
            else if (provaTurmaAlunoSituacaoBanco.AnoLetivo != provaTurmaAlunoSituacao.AnoLetivo ||
                provaTurmaAlunoSituacaoBanco.Inicio != provaTurmaAlunoSituacao.Inicio ||
                provaTurmaAlunoSituacaoBanco.Fim != provaTurmaAlunoSituacao.Fim ||
                provaTurmaAlunoSituacaoBanco.DreId != provaTurmaAlunoSituacao.DreId ||
                provaTurmaAlunoSituacaoBanco.UeId != provaTurmaAlunoSituacao.UeId ||
                provaTurmaAlunoSituacaoBanco.Ano != provaTurmaAlunoSituacao.Ano ||
                provaTurmaAlunoSituacaoBanco.Modalidade != provaTurmaAlunoSituacao.Modalidade ||
                provaTurmaAlunoSituacaoBanco.TurmaId != provaTurmaAlunoSituacao.TurmaId ||
                provaTurmaAlunoSituacaoBanco.AlunoNome != provaTurmaAlunoSituacao.AlunoNome ||
                provaTurmaAlunoSituacaoBanco.AlunoNomeSocial != provaTurmaAlunoSituacao.AlunoNomeSocial ||
                provaTurmaAlunoSituacaoBanco.AlunoDownload != provaTurmaAlunoSituacao.AlunoDownload ||
                provaTurmaAlunoSituacaoBanco.AlunoInicio != provaTurmaAlunoSituacao.AlunoInicio ||
                provaTurmaAlunoSituacaoBanco.AlunoFim != provaTurmaAlunoSituacao.AlunoFim ||
                provaTurmaAlunoSituacaoBanco.AlunoTempoMedio != provaTurmaAlunoSituacao.AlunoTempoMedio ||
                provaTurmaAlunoSituacaoBanco.AlunoQuestaoRespondida != provaTurmaAlunoSituacao.AlunoQuestaoRespondida)
            {
                await mediator.Send(new AlterarProvaAlunoResultadoCommand(provaTurmaAlunoSituacao));
            }

            return true;
        }
    }
}
