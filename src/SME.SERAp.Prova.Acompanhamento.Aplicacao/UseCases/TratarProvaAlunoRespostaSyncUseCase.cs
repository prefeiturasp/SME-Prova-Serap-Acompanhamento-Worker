using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaAlunoRespostaSyncUseCase : AbstractUseCase, ITratarProvaAlunoRespostaSyncUseCase
    {
        public TratarProvaAlunoRespostaSyncUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            // -> Respostas sincronizadas processo agendado
            var provaAlunoDto = mensagemRabbit.ObterObjetoMensagem<ProvaAlunoDto>();
            if (provaAlunoDto == null) return false;

            var provaAlunoRespostasSerap = await mediator.Send(new ObterProvaAlunoRespostaSerapQuery(provaAlunoDto.ProvaId, provaAlunoDto.AlunoRa));
            var provaAlunoRespostas = await mediator.Send(new ObterRespostasProvaAlunoQuery(provaAlunoDto.ProvaId, provaAlunoDto.AlunoRa));

            if (provaAlunoRespostasSerap != null && provaAlunoRespostasSerap.Any())
            {
                foreach (var provaAlunoRespostaSerap in provaAlunoRespostasSerap)
                {
                    var provaAlunoResposta = provaAlunoRespostas
                        .FirstOrDefault(x => 
                            x.ProvaId == provaAlunoRespostaSerap.ProvaId && 
                            x.AlunoRa == provaAlunoRespostaSerap.AlunoRa && 
                            x.QuestaoId == provaAlunoRespostaSerap.QuestaoId);

                    if (provaAlunoResposta == null)
                    {
                        await mediator.Send(new InserirProvaAlunoQuestaoRespostaCommand(provaAlunoRespostaSerap));
                    }
                    else if (provaAlunoRespostaSerap.AlternativaId != provaAlunoResposta.AlternativaId ||
                             provaAlunoRespostaSerap.Tempo != provaAlunoResposta.Tempo)
                    {
                        await mediator.Send(new AlterarProvaAlunoQuestaoRespostaCommand(provaAlunoResposta.Id, provaAlunoRespostaSerap));
                    }
                }
            }

            return true;
        }
    }
}
