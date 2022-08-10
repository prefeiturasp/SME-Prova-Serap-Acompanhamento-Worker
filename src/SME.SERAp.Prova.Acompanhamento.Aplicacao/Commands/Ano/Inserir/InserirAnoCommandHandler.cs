using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirAnoCommandHandler : IRequestHandler<InserirAnoCommand, bool>
    {
        private readonly IRepositorioAno repositorioAno;

        public InserirAnoCommandHandler(IRepositorioAno repositorioAno)
        {
            this.repositorioAno = repositorioAno ?? throw new ArgumentNullException(nameof(repositorioAno));
        }

        public async Task<bool> Handle(InserirAnoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioAno.InserirAsync(new Dominio.Entities.Ano(
                request.AnoDto.Id, 
                request.AnoDto.AnoLetivo, 
                request.AnoDto.Modalidade,
                request.AnoDto.UeId, 
                request.AnoDto.Nome));
        }
    }
}
