using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarAbrangenciaCommandHandler : IRequestHandler<AlterarAbrangenciaCommand, bool>
    {
        private readonly IRepositorioAbrangencia repositorioAbrangencia;

        public AlterarAbrangenciaCommandHandler(IRepositorioAbrangencia repositorioAbrangencia)
        {
            this.repositorioAbrangencia = repositorioAbrangencia ?? throw new ArgumentNullException(nameof(repositorioAbrangencia));
        }

        public async Task<bool> Handle(AlterarAbrangenciaCommand request, CancellationToken cancellationToken)
        {
            return await repositorioAbrangencia.AlterarAsync(new Dominio.Entities.Abrangencia(
                request.Abrangencia.UsuarioId,
                request.Abrangencia.Login,
                request.Abrangencia.Usuario,
                request.Abrangencia.GrupoId,
                request.Abrangencia.Grupo,
                request.Abrangencia.PermiteConsultar,
                request.Abrangencia.PermiteAlterar,
                request.Abrangencia.DreId,
                request.Abrangencia.UeId,
                request.Abrangencia.TurmaId
                ));
        }
    }
}
