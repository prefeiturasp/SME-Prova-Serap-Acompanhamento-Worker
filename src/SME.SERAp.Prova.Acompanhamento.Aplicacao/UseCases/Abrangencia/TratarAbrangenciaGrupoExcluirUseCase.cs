using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarAbrangenciaGrupoExcluirUseCase : AbstractUseCase, ITratarAbrangenciaGrupoExcluirUseCase
    {
        public TratarAbrangenciaGrupoExcluirUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var grupoUsuarioIds = mensagemRabbit.ObterObjetoMensagem<GrupoExcluirCoressoDto>();
            if (grupoUsuarioIds == null) return false;

            var abrangenciaIdsExcluir = await mediator.Send(new ObterAbrangenciaIdsPorGrupoDiferenteUsuarioIdsQuery(grupoUsuarioIds.GrupoId, grupoUsuarioIds.UsuarioIds));

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
