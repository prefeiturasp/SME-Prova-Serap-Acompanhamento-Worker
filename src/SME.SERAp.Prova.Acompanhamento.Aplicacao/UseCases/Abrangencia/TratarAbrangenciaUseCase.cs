using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarAbrangenciaUseCase : AbstractUseCase, ITratarAbrangenciaUseCase
    {
        public TratarAbrangenciaUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var abrangencia = mensagemRabbit.ObterObjetoMensagem<Dominio.Entities.Abrangencia>();
            if (abrangencia == null) return false;

            var abrangenciaBanco = await mediator.Send(new ObterAbrangenciaPorIdQuery(abrangencia.Id));
            if (abrangenciaBanco == null)
            {
                await mediator.Send(new InserirAbrangenciaCommand(abrangencia));
            }
            else if (abrangenciaBanco.UsuarioId != abrangencia.UsuarioId ||
                     abrangenciaBanco.Login != abrangencia.Login ||
                     abrangenciaBanco.Usuario != abrangencia.Usuario ||
                     abrangenciaBanco.GrupoId != abrangencia.GrupoId ||
                     abrangenciaBanco.Grupo != abrangencia.Grupo ||
                     abrangenciaBanco.PermiteConsultar != abrangencia.PermiteConsultar ||
                     abrangenciaBanco.PermiteAlterar != abrangencia.PermiteAlterar ||
                     abrangenciaBanco.DreId != abrangencia.DreId ||
                     abrangenciaBanco.UeId != abrangencia.UeId ||
                     abrangenciaBanco.TurmaId != abrangencia.TurmaId)
            {
                await mediator.Send(new AlterarAbrangenciaCommand(abrangencia));
            }

            return true;
        }
    }
}
