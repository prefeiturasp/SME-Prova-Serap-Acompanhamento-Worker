using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    internal class InserirUeCommandHandler : IRequestHandler<InserirUeCommand, bool>
    {
        private readonly IRepositorioUe repositorioUe;

        public InserirUeCommandHandler(IRepositorioUe repositorioUe)
        {
            this.repositorioUe = repositorioUe ?? throw new ArgumentNullException(nameof(repositorioUe));
        }

        public async Task<bool> Handle(InserirUeCommand request, CancellationToken cancellationToken)
        {
            return await repositorioUe.InserirAsync(new Dominio.Entities.Ue(request.UeDto.Id, request.UeDto.DreId, request.UeDto.Codigo, request.UeDto.Nome));
        }
    }
}
