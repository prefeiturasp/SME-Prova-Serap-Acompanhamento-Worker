using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirDreCommandHandler : IRequestHandler<InserirDreCommand, bool>
    {
        private readonly IRepositorioDre repositorioDre;

        public InserirDreCommandHandler(IRepositorioDre repositorioDre)
        {
            this.repositorioDre = repositorioDre;
        }

        public async Task<bool> Handle(InserirDreCommand request, CancellationToken cancellationToken)
        {
            return await repositorioDre.InserirAsync(new Dre(request.DreDto.Id, request.DreDto.Codigo, request.DreDto.Abreviacao, request.DreDto.Nome));
        }
    }
}
