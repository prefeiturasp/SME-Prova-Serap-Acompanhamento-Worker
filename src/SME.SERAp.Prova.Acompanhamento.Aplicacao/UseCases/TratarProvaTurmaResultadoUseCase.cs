using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaTurmaResultadoUseCase : AbstractUseCase, ITratarProvaTurmaResultadoUseCase
    {
        public TratarProvaTurmaResultadoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provaTurmaResultado = mensagemRabbit.ObterObjetoMensagem<ProvaTurmaResultado>();
            if (provaTurmaResultado == null) return false;

            var provaTurmaResultadoBanco = await mediator.Send(new ObterProvaTurmaResultadoQuery(provaTurmaResultado.ProvaId, provaTurmaResultado.TurmaId));

            if (provaTurmaResultadoBanco == null)
            {
                await mediator.Send(new InserirProvaTurmaResultadoCommand(provaTurmaResultado));
            }
            else if (provaTurmaResultadoBanco.ProvaId != provaTurmaResultado.ProvaId ||
                provaTurmaResultadoBanco.DreId != provaTurmaResultado.DreId ||
                provaTurmaResultadoBanco.UeId != provaTurmaResultado.UeId ||
                provaTurmaResultadoBanco.TurmaId != provaTurmaResultado.TurmaId ||
                provaTurmaResultadoBanco.Ano != provaTurmaResultado.Ano ||
                provaTurmaResultadoBanco.Modalidade != provaTurmaResultado.Modalidade ||
                provaTurmaResultadoBanco.AnoLetivo != provaTurmaResultado.AnoLetivo ||
                provaTurmaResultadoBanco.Inicio != provaTurmaResultado.Inicio ||
                provaTurmaResultadoBanco.Fim != provaTurmaResultado.Fim ||
                provaTurmaResultadoBanco.Descricao != provaTurmaResultado.Descricao ||
                provaTurmaResultadoBanco.TotalAlunos != provaTurmaResultado.TotalAlunos ||
                provaTurmaResultadoBanco.TotalIniciadas != provaTurmaResultado.TotalIniciadas ||
                provaTurmaResultadoBanco.TotalNaoFinalizados != provaTurmaResultado.TotalNaoFinalizados ||
                provaTurmaResultadoBanco.TotalFinalizados != provaTurmaResultado.TotalFinalizados ||
                provaTurmaResultadoBanco.QuantidadeQuestoes != provaTurmaResultado.QuantidadeQuestoes ||
                provaTurmaResultadoBanco.TotalQuestoes != provaTurmaResultado.TotalQuestoes ||
                provaTurmaResultadoBanco.QuestoesRespondidas != provaTurmaResultado.QuestoesRespondidas ||
                provaTurmaResultadoBanco.TempoMedio != provaTurmaResultado.TempoMedio)
            {
                await mediator.Send(new AlterarProvaTurmaResultadoCommand(provaTurmaResultado));
            }

            return true;
        }
    }
}
