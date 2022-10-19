using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarAbrangenciaGrupoUsuarioExcluirUseCase : AbstractUseCase, ITratarAbrangenciaGrupoUsuarioExcluirUseCase
    {
        public TratarAbrangenciaGrupoUsuarioExcluirUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var grupoUsuarioAbrangencias = mensagemRabbit.ObterObjetoMensagem<GrupoUsuarioExcluirCoressoDto>();
            if (grupoUsuarioAbrangencias == null) return false;

            var abrangenciaIdsExcluir = await mediator.Send(new ObterAbrangenciaIdsDiferenteAbrangenciasQuery(grupoUsuarioAbrangencias.GrupoId, grupoUsuarioAbrangencias.UsuarioId, grupoUsuarioAbrangencias.AbrangenciaIds));
            if (abrangenciaIdsExcluir != null && abrangenciaIdsExcluir.Any())
            {
                foreach (var abrangenciaId in abrangenciaIdsExcluir)
                {
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.AbrangenciaExcluir, abrangenciaId));
                }
            }

            return true;
        }
    }
}
