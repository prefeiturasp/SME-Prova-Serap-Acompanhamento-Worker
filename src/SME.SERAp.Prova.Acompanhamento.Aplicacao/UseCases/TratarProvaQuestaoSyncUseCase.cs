using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaQuestaoSyncUseCase : AbstractUseCase, ITratarProvaQuestaoSyncUseCase
    {
        public TratarProvaQuestaoSyncUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provaId = long.Parse(mensagemRabbit.Mensagem.ToString());

            var questoesIds = await mediator.Send(new ObterQuestoesIdsPorProvaIdQuery(provaId));
            var provaQuestoes = await mediator.Send(new ObterProvaQuestaoPorProvaIdQuery(provaId));

            var questoesIdsInserir = questoesIds.Where(q => !provaQuestoes.Any(pq => pq.QuestaoId == q));
            var questoesIdsExcluir = provaQuestoes.Where(pq => !questoesIds.Any(q => q == pq.QuestaoId)).Select(pq => pq.QuestaoId);

            foreach (long id in questoesIdsInserir)
            {
                var dto = new ProvaQuestaoDto(provaId, id, AcaoCrud.Inserir);
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaQuestaoTratar, dto));
            }

            foreach (long id in questoesIdsExcluir)
            {
                var dto = new ProvaQuestaoDto(provaId, id, AcaoCrud.Excluir);
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaQuestaoTratar, dto));
            }

            return true;
        }
    }
}
