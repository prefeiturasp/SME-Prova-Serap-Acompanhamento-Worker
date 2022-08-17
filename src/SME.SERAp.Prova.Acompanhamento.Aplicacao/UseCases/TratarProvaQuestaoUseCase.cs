using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaQuestaoUseCase : AbstractUseCase, ITratarProvaQuestaoUseCase
    {
        public TratarProvaQuestaoUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provaQuestaoDto = mensagemRabbit.ObterObjetoMensagem<ProvaQuestaoDto>();

            if (provaQuestaoDto == null) return false;

            var provaQuestao = new ProvaQuestao(provaQuestaoDto.ProvaId, provaQuestaoDto.QuestaoId);

            if (provaQuestaoDto.Acao == AcaoCrud.Inserir)
                return await mediator.Send(new InserirProvaQuestaoCommand(provaQuestao));
            if (provaQuestaoDto.Acao == AcaoCrud.Excluir)
                return await mediator.Send(new ExcluirProvaQuestaoCommand(provaQuestao));

            return true;
        }
    }
}
