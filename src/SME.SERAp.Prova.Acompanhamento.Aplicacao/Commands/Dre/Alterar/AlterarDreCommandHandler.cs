using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarDreCommandHandler : IRequestHandler<AlterarDreCommand, bool>
    {
        private readonly IRepositorioDre repositorioDre;

        public AlterarDreCommandHandler(IRepositorioDre repositorioDre)
        {
            this.repositorioDre = repositorioDre;
        }

        public async Task<bool> Handle(AlterarDreCommand request, CancellationToken cancellationToken)
        {
            return await repositorioDre.AlterarAsync(new Dre(request.DreDto.Id, request.DreDto.Codigo, request.DreDto.Abreviacao, request.DreDto.Nome));
        }
    }
}
