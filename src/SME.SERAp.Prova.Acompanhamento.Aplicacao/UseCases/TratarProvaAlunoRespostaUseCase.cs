using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaAlunoRespostaUseCase : AbstractUseCase, ITratarProvaAlunoRespostaUseCase
    {
        public TratarProvaAlunoRespostaUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            // -> Respostas sincronizadas em tempo de execução das provas

            var provaAlunoRespostaDto = mensagemRabbit.ObterObjetoMensagem<ProvaAlunoRespostaDto>();
            if (provaAlunoRespostaDto == null) return false;

            if (provaAlunoRespostaDto.AlternativaId == null && provaAlunoRespostaDto.Tempo == null)
                return false;

            if (provaAlunoRespostaDto.ProvaId == 0)
            {
                var provaQuestao = await mediator.Send(new ObterProvaQuestaoPorQuestaoIdQuery(provaAlunoRespostaDto.QuestaoId));

                if (provaQuestao == null || provaQuestao.ProvaId == 0) return false;

                provaAlunoRespostaDto.ProvaId = provaQuestao.ProvaId;
            }

            var provaAlunoResposta = await mediator.Send(new ObterProvaAlunoRespostaQuery(provaAlunoRespostaDto.ProvaId, provaAlunoRespostaDto.AlunoRa, provaAlunoRespostaDto.QuestaoId));
            if (provaAlunoResposta == null)
            {
                await mediator.Send(new InserirProvaAlunoQuestaoRespostaCommand(provaAlunoRespostaDto));
            }
            else if (provaAlunoRespostaDto.AlternativaId != provaAlunoResposta.AlternativaId ||
                     provaAlunoRespostaDto.Tempo != provaAlunoResposta.Tempo)
            {
                if (provaAlunoRespostaDto.Consolidar)
                    provaAlunoRespostaDto.Tempo += provaAlunoResposta.Tempo;

                await mediator.Send(new AlterarProvaAlunoQuestaoRespostaCommand(provaAlunoResposta.Id, provaAlunoRespostaDto));
            }

            if (provaAlunoRespostaDto.Consolidar)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaAlunoRespostaConsolidar, provaAlunoRespostaDto));

            return true;
        }
    }
}
