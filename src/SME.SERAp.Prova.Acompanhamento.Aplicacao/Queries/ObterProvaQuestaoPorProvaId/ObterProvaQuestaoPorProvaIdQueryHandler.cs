using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaQuestaoPorProvaIdQueryHandler : IRequestHandler<ObterProvaQuestaoPorProvaIdQuery, IEnumerable<ProvaQuestao>>
    {
        private readonly IRepositorioProvaQuestao repositorioProvaQuestao;
        public ObterProvaQuestaoPorProvaIdQueryHandler(IRepositorioProvaQuestao repositorioProvaQuestao)
        {
            this.repositorioProvaQuestao = repositorioProvaQuestao ?? throw new ArgumentNullException(nameof(repositorioProvaQuestao));
        }

        public async Task<IEnumerable<ProvaQuestao>> Handle(ObterProvaQuestaoPorProvaIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProvaQuestao.ObterPorProvaIdAsync(request.ProvaId);
        }
    }
}
