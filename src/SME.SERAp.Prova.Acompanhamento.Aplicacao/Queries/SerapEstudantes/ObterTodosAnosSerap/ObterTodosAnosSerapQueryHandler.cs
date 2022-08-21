using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTodosAnosSerapQueryHandler : IRequestHandler<ObterTodosAnosSerapQuery, IEnumerable<AnoDto>>
    {
        private readonly IRepositorioSerapAno repositorioSerapAno;

        public ObterTodosAnosSerapQueryHandler(IRepositorioSerapAno repositorioSerapAno)
        {
            this.repositorioSerapAno = repositorioSerapAno ?? throw new ArgumentNullException(nameof(repositorioSerapAno));
        }

        public async Task<IEnumerable<AnoDto>> Handle(ObterTodosAnosSerapQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSerapAno.ObterTodosAsync();
        }
    }
}
