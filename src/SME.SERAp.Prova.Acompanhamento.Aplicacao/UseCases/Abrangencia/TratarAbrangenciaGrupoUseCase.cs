using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarAbrangenciaGrupoUseCase : AbstractUseCase, ITratarAbrangenciaGrupoUseCase
    {
        public TratarAbrangenciaGrupoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var grupo = mensagemRabbit.ObterObjetoMensagem<GrupoCoressoDto>();
            if (grupo == null) return false;

            var usuarios = await mediator.Send(new ObterUsuariosCoressoPorGrupoQuery(grupo.Id));
            if (usuarios != null && usuarios.Any())
            {
                foreach (var usuario in usuarios)
                {
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.AbrangenciaGrupoUsuarioTratar, new GrupoUsuarioCoressoDto(grupo, usuario)));
                }

                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.AbrangenciaGrupoExcluirTratar, new GrupoExcluirCoressoDto(grupo.Id, usuarios.Select(t => t.Id).ToArray())));
            }

            return true;
        }
    }
}