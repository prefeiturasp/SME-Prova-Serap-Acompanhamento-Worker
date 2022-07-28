using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTodasUesPorDreIdSerapQuery : IRequest<IEnumerable<UeDto>>
    {
        public ObterTodasUesPorDreIdSerapQuery(long dreId)
        {
            DreId = dreId;
        }

        public long DreId { get; set; }
    }
}
