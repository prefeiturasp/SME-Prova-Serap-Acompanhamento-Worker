using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarProvaCommandHandler : IRequestHandler<AlterarProvaCommand, bool>
    {
        private readonly IRepositorioProva repositorioProva;

        public AlterarProvaCommandHandler(IRepositorioProva repositorioProva)
        {
            this.repositorioProva = repositorioProva ?? throw new ArgumentNullException(nameof(repositorioProva));
        }

        public async Task<bool> Handle(AlterarProvaCommand request, CancellationToken cancellationToken)
        {
            return await repositorioProva.AlterarAsync(new Dominio.Entities.Prova(
                request.ProvaDto.Id,
                request.ProvaDto.Codigo,
                request.ProvaDto.Descricao,
                request.ProvaDto.Modalidade,
                request.ProvaDto.Inicio.Year,
                request.ProvaDto.Inicio,
                request.ProvaDto.Fim
                ));
        }
    }
}
