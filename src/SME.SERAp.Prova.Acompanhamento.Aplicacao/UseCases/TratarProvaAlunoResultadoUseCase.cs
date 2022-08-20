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
            var provaAlunoResultado = mensagemRabbit.ObterObjetoMensagem<ProvaAlunoResultado>();
            if (provaAlunoResultado == null) return false;

            var provaAlunoResultadoBanco = await mediator.Send(new ObterProvaAlunoIdResultadoQuery(provaAlunoResultado.ProvaId, provaAlunoResultado.AlunoId));

            if (provaAlunoResultadoBanco == null)
            {
                await mediator.Send(new InserirProvaAlunoResultadoCommand(provaAlunoResultado));
            }
            else if (provaAlunoResultadoBanco.AnoLetivo != provaAlunoResultado.AnoLetivo ||
                provaAlunoResultadoBanco.Inicio != provaAlunoResultado.Inicio ||
                provaAlunoResultadoBanco.Fim != provaAlunoResultado.Fim ||
                provaAlunoResultadoBanco.DreId != provaAlunoResultado.DreId ||
                provaAlunoResultadoBanco.UeId != provaAlunoResultado.UeId ||
                provaAlunoResultadoBanco.Ano != provaAlunoResultado.Ano ||
                provaAlunoResultadoBanco.Modalidade != provaAlunoResultado.Modalidade ||
                provaAlunoResultadoBanco.TurmaId != provaAlunoResultado.TurmaId ||
                provaAlunoResultadoBanco.AlunoNome != provaAlunoResultado.AlunoNome ||
                provaAlunoResultadoBanco.AlunoNomeSocial != provaAlunoResultado.AlunoNomeSocial ||
                provaAlunoResultadoBanco.AlunoDownload != provaAlunoResultado.AlunoDownload ||
                provaAlunoResultadoBanco.AlunoSituacao != provaAlunoResultado.AlunoSituacao ||
                provaAlunoResultadoBanco.AlunoInicio != provaAlunoResultado.AlunoInicio ||
                provaAlunoResultadoBanco.AlunoFim != provaAlunoResultado.AlunoFim ||
                provaAlunoResultadoBanco.AlunoTempoMedio != provaAlunoResultado.AlunoTempoMedio ||
                provaAlunoResultadoBanco.AlunoQuestaoRespondida != provaAlunoResultado.AlunoQuestaoRespondida)
            {
                provaAlunoResultado.Id = provaAlunoResultadoBanco.Id;
                await mediator.Send(new AlterarProvaAlunoResultadoCommand(provaAlunoResultado));
            }

            return true;
        }
    }
}
