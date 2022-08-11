using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaAlunoRespostaUseCase : AbstractUseCase, ITratarProvaAlunoRespostaUseCase
    {
        public TratarProvaAlunoRespostaUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provaAlunoRespostaDto = mensagemRabbit.ObterObjetoMensagem<ProvaAlunoRespostaDto>();
            if (provaAlunoRespostaDto == null) return false;

            if (provaAlunoRespostaDto.ProvaId == 0)
            {
                var provaQuestao = await mediator.Send(new ObterProvaQuestaoPorQuestaoIdQuery(provaAlunoRespostaDto.QuestaoId));
                provaAlunoRespostaDto.ProvaId = provaQuestao.ProvaId;
            }

            var provaAlunoResposta = await mediator.Send(new ObterProvaAlunoRespostaQuery(provaAlunoRespostaDto.ProvaId, provaAlunoRespostaDto.AlunoRa, provaAlunoRespostaDto.QuestaoId));
            if (provaAlunoResposta == null)
            {
                await mediator.Send(new InserirProvaAlunoQuestaoRespostaCommand(provaAlunoRespostaDto));
            }
            else if (provaAlunoRespostaDto.AlternativaId != provaAlunoResposta.AlternativaId ||
                     provaAlunoResposta.Tempo != provaAlunoResposta.Tempo)
            {
                await mediator.Send(new AlterarProvaAlunoQuestaoRespostaCommand(provaAlunoRespostaDto));
            }

            if (provaAlunoRespostaDto.Consolidar)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaAlunoRespostaConsolidar, provaAlunoRespostaDto));

            return true;
        }
    }
}
