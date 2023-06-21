using MediatR;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterDeficienciasPorProvaIdQuery : IRequest<IEnumerable<long>>
    {
        public ObterDeficienciasPorProvaIdQuery(long provaId)
        {
            ProvaId = provaId;
        }

        public long ProvaId { get; set; }
    }
}
