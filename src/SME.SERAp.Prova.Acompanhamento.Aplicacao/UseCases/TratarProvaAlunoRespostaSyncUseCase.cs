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
            var provaAlunoDto = mensagemRabbit.ObterObjetoMensagem<ProvaAlunoDto>();
            if (provaAlunoDto == null) return false;

            var respostas = await mediator.Send(new ObterProvaAlunoRespostaSerapQuery(provaAlunoDto.ProvaId, provaAlunoDto.AlunoRa));

            if (respostas != null && respostas.Any())
            {
                foreach (var resposta in respostas)
                {
                    resposta.Consolidar = false;
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaAlunoRespostaTratar, resposta));
                }
            }

            return true;
        }
    }
}
