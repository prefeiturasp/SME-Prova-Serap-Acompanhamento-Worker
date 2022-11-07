using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAlunosTurmaSerapQuery : IRequest<IEnumerable<AlunoDto>>
    {
        public ObterAlunosTurmaSerapQuery(long provaId, long turmaId, bool deficiente, long[] deficiencias)
        {
            ProvaId = provaId;
            TurmaId = turmaId;
            Deficiente = deficiente;
            Deficiencias = deficiencias;
        }

        public long ProvaId { get; set; }
        public long TurmaId { get; set; }
        public bool Deficiente { get; set; }
        public long[] Deficiencias { get; set; }
    }
}
