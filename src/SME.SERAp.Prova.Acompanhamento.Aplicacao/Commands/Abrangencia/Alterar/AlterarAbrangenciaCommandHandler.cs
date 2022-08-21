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
                request.AbrangenciaDto.Id,
                request.AbrangenciaDto.Login,
                request.AbrangenciaDto.Usuario,
                request.AbrangenciaDto.CoressoId,
                request.AbrangenciaDto.Grupo,
                request.AbrangenciaDto.DreId,
                request.AbrangenciaDto.UeId,
                request.AbrangenciaDto.TurmaId
                ));
        }
    }
}
