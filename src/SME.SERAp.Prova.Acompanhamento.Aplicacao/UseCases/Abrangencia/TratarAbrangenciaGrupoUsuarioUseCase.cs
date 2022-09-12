using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Constraints;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarAbrangenciaGrupoUsuarioUseCase : AbstractUseCase, ITratarAbrangenciaGrupoUsuarioUseCase
    {
        public TratarAbrangenciaGrupoUsuarioUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var grupoUsuario = mensagemRabbit.ObterObjetoMensagem<GrupoUsuarioCoressoDto>();
            if (grupoUsuario == null) return false;

            if (Perfis.PerfilEhAdministrador(grupoUsuario.Grupo.Id))
            {
                var abrangencia = await PublicarAbrangencia(grupoUsuario, 0, 0, 0);
                await TratarAbrangenciaGrupoUsuarioExcluir(new List<Abrangencia>() { abrangencia });
                return true;
            }

            if (Perfis.PerfilEhProfessor(grupoUsuario.Grupo.Id))
            {
                var atribuicoesTurmasEol = await mediator.Send(new ObterAtribuicaoTurmaPorRegistroFuncionalQuery(grupoUsuario.Usuario.Login));
                if (atribuicoesTurmasEol != null && atribuicoesTurmasEol.Any())
                {
                    await TratarAbrangenciaGrupoUsuarioExcluir(await TratarAbrangencia(grupoUsuario, atribuicoesTurmasEol));
                    return true;
                }
            }

            var abrangenciasCoresso = await mediator.Send(new ObterAbrangenciaCoressoPorUsuarioGrupoQuery(grupoUsuario.Grupo.Id, grupoUsuario.Usuario.Id));
            if (abrangenciasCoresso != null && abrangenciasCoresso.Any())
            {
                await TratarAbrangenciaGrupoUsuarioExcluir(await TratarAbrangencia(grupoUsuario, abrangenciasCoresso));
                return true;
            }

            var atribuicoesDreUeEol = await mediator.Send(new ObterAtribuicaoDreUePorRegistroFuncionalQuery(grupoUsuario.Usuario.Login));
            if (atribuicoesDreUeEol != null && atribuicoesDreUeEol.Any())
            {
                await TratarAbrangenciaGrupoUsuarioExcluir(await TratarAbrangencia(grupoUsuario, atribuicoesDreUeEol));
            }

            return true;
        }

        private async Task TratarAbrangenciaGrupoUsuarioExcluir(IEnumerable<Abrangencia> abrangencias)
        {
            if (abrangencias != null && abrangencias.Any())
            {
                var GrupoUsuarioExcluirCoressoDto = new GrupoUsuarioExcluirCoressoDto(
                    abrangencias.FirstOrDefault().GrupoId,
                    abrangencias.FirstOrDefault().UsuarioId,
                    abrangencias.Select(t => t.Id).ToArray());

                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.AbrangenciaGrupoUsuarioExcluirTratar, GrupoUsuarioExcluirCoressoDto));
            }
        }

        private async Task<IEnumerable<Abrangencia>> TratarAbrangencia(GrupoUsuarioCoressoDto grupoUsuario, IEnumerable<string> abrangenciasCodigo)
        {
            Ue ue;
            Dre dre;
            Turma turma;

            var abrangencias = new List<Abrangencia>();

            foreach (var codigo in abrangenciasCodigo.Distinct())
            {
                long turmaId = 0;
                long ueId = 0;
                long dreId = 0;

                turma = await mediator.Send(new ObterTurmaPorCodigoQuery(long.Parse(codigo)));
                if (turma != null)
                {
                    ue = await mediator.Send(new ObterUePorIdQuery(turma.UeId.ToString()));

                    dreId = ue.DreId;
                    ueId = long.Parse(ue.Id);
                    turmaId = long.Parse(turma.Id);

                    abrangencias.Add(await PublicarAbrangencia(grupoUsuario, dreId, ueId, turmaId));
                    continue;
                }

                ue = await mediator.Send(new ObterUePorCodigoQuery(long.Parse(codigo)));
                if (ue != null)
                {
                    dreId = ue.DreId;
                    ueId = long.Parse(ue.Id);

                    abrangencias.Add(await PublicarAbrangencia(grupoUsuario, dreId, ueId, turmaId));
                    continue;
                }

                dre = await mediator.Send(new ObterDrePorCodigoQuery(long.Parse(codigo)));
                if (dre != null)
                {
                    dreId = long.Parse(dre.Id);
                    abrangencias.Add(await PublicarAbrangencia(grupoUsuario, dreId, ueId, turmaId));
                    continue;
                }
            }

            return abrangencias;
        }

        private async Task<Abrangencia> PublicarAbrangencia(GrupoUsuarioCoressoDto grupoUsuarioCoressoDto, long dreId, long ueId, long turmaId)
        {
            Abrangencia abrangencia = new(
                grupoUsuarioCoressoDto.Usuario.Id,
                grupoUsuarioCoressoDto.Usuario.Login,
                grupoUsuarioCoressoDto.Usuario.Nome,
                grupoUsuarioCoressoDto.Grupo.Id,
                grupoUsuarioCoressoDto.Grupo.Nome,
                grupoUsuarioCoressoDto.Grupo.PermiteConsultar,
                grupoUsuarioCoressoDto.Grupo.PermiteAlterar,
                dreId,
                ueId,
                turmaId);

            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.AbrangenciaTratar, abrangencia));

            return abrangencia;
        }
    }
}
