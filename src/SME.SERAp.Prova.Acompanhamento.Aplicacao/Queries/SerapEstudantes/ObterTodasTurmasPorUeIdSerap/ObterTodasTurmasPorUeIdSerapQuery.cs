using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTodasTurmasPorUeIdSerapQuery : IRequest<IEnumerable<TurmaDto>>
    {
        public ObterTodasTurmasPorUeIdSerapQuery(long ueId)
        {
            UeId = ueId;
        }

        public long UeId { get; set; }
    }
}
