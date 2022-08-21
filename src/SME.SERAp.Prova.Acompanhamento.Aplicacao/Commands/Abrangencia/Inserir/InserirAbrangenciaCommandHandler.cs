using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirAbrangenciaCommandHandler : IRequestHandler<InserirAbrangenciaCommand, bool>
    {
        private readonly IRepositorioAbrangencia repositorioAbrangencia;

        public InserirAbrangenciaCommandHandler(IRepositorioAbrangencia repositorioAbrangencia)
        {
            this.repositorioAbrangencia = repositorioAbrangencia ?? throw new ArgumentNullException(nameof(repositorioAbrangencia));
        }

        public async Task<bool> Handle(InserirAbrangenciaCommand request, CancellationToken cancellationToken)
        {
            return await repositorioAbrangencia.InserirAsync(new Dominio.Entities.Abrangencia(
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
