using MediatR;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterQuestoesIdsPorProvaIdQuery : IRequest<IEnumerable<long>>
    {
        public ObterQuestoesIdsPorProvaIdQuery(long provaId)
        {
            ProvaId = provaId;
        }

        public long ProvaId { get; set; }

    }
}
