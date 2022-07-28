using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    internal class AlterarUeCommandHandler : IRequestHandler<AlterarUeCommand, bool>
    {
        private readonly IRepositorioUe repositorioUe;

        public AlterarUeCommandHandler(IRepositorioUe repositorioUe)
        {
            this.repositorioUe = repositorioUe ?? throw new ArgumentNullException(nameof(repositorioUe));
        }

        public async Task<bool> Handle(AlterarUeCommand request, CancellationToken cancellationToken)
        {
            return await repositorioUe.AlterarAsync(new Dominio.Entities.Ue(request.UeDto.Id, request.UeDto.DreId, request.UeDto.Codigo, request.UeDto.Nome));
        }
    }
}
