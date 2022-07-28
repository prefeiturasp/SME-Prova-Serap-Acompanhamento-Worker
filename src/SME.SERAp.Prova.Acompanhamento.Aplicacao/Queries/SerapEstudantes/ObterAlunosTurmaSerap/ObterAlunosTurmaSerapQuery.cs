using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAlunosTurmaSerapQuery : IRequest<IEnumerable<AlunoDto>>
    {
        public ObterAlunosTurmaSerapQuery(long turmaId, DateTime provaInicio, DateTime provaFim)
        {
            TurmaId = turmaId;
            ProvaInicio = provaInicio;
            ProvaFim = provaFim;
        }

        public long TurmaId { get; set; }
        public DateTime ProvaInicio { get; set; }
        public DateTime ProvaFim { get; set; }
    }
}
