using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAnoPorIdQueryHandler : IRequestHandler<ObterAnoPorIdQuery, Ano>
    {
        private readonly IRepositorioAno repositorioAno;

        public ObterAnoPorIdQueryHandler(IRepositorioAno repositorioAno)
        {
            this.repositorioAno = repositorioAno ?? throw new ArgumentNullException(nameof(repositorioAno));
        }

        public async Task<Ano> Handle(ObterAnoPorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAno.ObterPorIdAsync(request.Id);
        }
    }
}
