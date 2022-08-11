using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaQuestaoPorProvaIdQuery : IRequest<IEnumerable<ProvaQuestao>>
    {
        public ObterProvaQuestaoPorProvaIdQuery(long provaId)
        {
            ProvaId = provaId;
        }

        public long ProvaId { get; set; }

    }
}
