using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterQuestoesIdsPorProvaIdQueryHandler : IRequestHandler<ObterQuestoesIdsPorProvaIdQuery, IEnumerable<long>>
    {
        private readonly IRepositorioSerapQuestao repositorioSerapQuestao;

        public ObterQuestoesIdsPorProvaIdQueryHandler(IRepositorioSerapQuestao repositorioSerapQuestao)
        {
            this.repositorioSerapQuestao = repositorioSerapQuestao ?? throw new ArgumentNullException(nameof(repositorioSerapQuestao));
        }

        public async Task<IEnumerable<long>> Handle(ObterQuestoesIdsPorProvaIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSerapQuestao.ObterQuestoesIdsPorProvaId(request.ProvaId);
        }

    }
}
