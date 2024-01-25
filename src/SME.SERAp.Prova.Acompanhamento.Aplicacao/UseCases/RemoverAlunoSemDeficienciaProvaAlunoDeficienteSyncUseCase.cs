using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class RemoverAlunoSemDeficienciaProvaAlunoDeficienteSyncUseCase : AbstractUseCase, IRemoverAlunoSemDeficienciaProvaAlunoDeficienteSyncUseCase
    {
        public RemoverAlunoSemDeficienciaProvaAlunoDeficienteSyncUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provas = await mediator.Send(new ObterProvasParaDeficientesQuery());
            if (provas == null || !provas.Any())
                return false;

            foreach (var prova in provas)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.RemoverAlunoSemDeficienciaProvaAlunoDeficiente, prova.Id));

            return true;
        }
    }
}