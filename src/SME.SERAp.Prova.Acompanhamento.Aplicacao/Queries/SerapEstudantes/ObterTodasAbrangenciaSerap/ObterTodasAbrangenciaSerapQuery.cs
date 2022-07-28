using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTodasAbrangenciaSerapQuery : IRequest<IEnumerable<AbrangenciaDto>>
    {
        public ObterTodasAbrangenciaSerapQuery(Guid coressoId)
        {
            CoressoId = coressoId;
        }

        public Guid CoressoId { get; set; }
    }
}
