using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaAlunoResultadoPorProvaTurmaQuery : IRequest<IEnumerable<ProvaAlunoResultado>>
    {
        public ObterProvaAlunoResultadoPorProvaTurmaQuery(long provaId, long turmaId)
        {
            ProvaId = provaId;
            TurmaId = turmaId;
        }

        public long ProvaId { get; set; }
        public long TurmaId { get; set; }
    }
}
