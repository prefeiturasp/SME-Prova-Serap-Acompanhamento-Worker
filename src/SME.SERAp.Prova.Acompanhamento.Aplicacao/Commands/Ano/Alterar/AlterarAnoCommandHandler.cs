using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarAnoCommandHandler : IRequestHandler<AlterarAnoCommand, bool>
    {
        private readonly IRepositorioAno repositorioAno;

        public AlterarAnoCommandHandler(IRepositorioAno repositorioAno)
        {
            this.repositorioAno = repositorioAno ?? throw new ArgumentNullException(nameof(repositorioAno));
        }

        public async Task<bool> Handle(AlterarAnoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioAno.AlterarAsync(new Dominio.Entities.Ano(request.AnoDto.Id, request.AnoDto.AnoLetivo, request.AnoDto.UeId, request.AnoDto.Nome));
        }
    }
}
