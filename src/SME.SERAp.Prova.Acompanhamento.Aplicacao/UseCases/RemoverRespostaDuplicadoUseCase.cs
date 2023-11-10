using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class RemoverRespostaDuplicadoUseCase : AbstractUseCase, IRemoverRespostaDuplicadoUseCase
    {
        public RemoverRespostaDuplicadoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var turmaId = long.Parse(mensagemRabbit.Mensagem.ToString());

            var provasAlunosResultados = await mediator.Send(new ObterProvaAlunoResultadoPorTurmaIdQuery(turmaId));
            if (provasAlunosResultados == null || !provasAlunosResultados.Any())
                return false;

            foreach (var provaAlunoResultado in provasAlunosResultados)
            {
                var respostas = await mediator.Send(new ObterRespostasProvaAlunoQuery(provaAlunoResultado.ProvaId, provaAlunoResultado.AlunoRa));
                if(respostas == null || !respostas.Any()) 
                    continue;

                var duplicados = respostas
                    .GroupBy(c => new { c.ProvaId, c.AlunoRa, c.QuestaoId })
                    .Where(c => c.Count() > 1)
                    .Select(c => c.Key);

                foreach (var duplicado in duplicados)
                {
                    var respostasRemover = respostas
                        .Where(c => c.ProvaId == duplicado.ProvaId
                                    && c.AlunoRa == duplicado.AlunoRa
                                    && c.QuestaoId == duplicado.QuestaoId);

                    var primeiroRegistro = true;
                    foreach (var provaAlunoResposta in respostasRemover)
                    {
                        if (primeiroRegistro)
                        {
                            primeiroRegistro = false;
                            continue;
                        }

                        var dtoVazio = new ProvaAlunoRespostaDto();
                        await mediator.Send(new AlterarProvaAlunoQuestaoRespostaCommand(provaAlunoResposta.Id, dtoVazio));
                    }
                }

                if (duplicados.Any())
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaAlunoRespostaConsolidar, new ProvaAlunoDto { AlunoRa = provaAlunoResultado.AlunoRa, ProvaId = provaAlunoResultado.ProvaId }));
            }

            return true;
        }
    }
}