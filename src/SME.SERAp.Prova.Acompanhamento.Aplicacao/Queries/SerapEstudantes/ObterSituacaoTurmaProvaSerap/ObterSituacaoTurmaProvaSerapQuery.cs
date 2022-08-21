using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries.SerapEstudantes.ObterSituacaoTurmaProvaSerap
{
    public class ObterSituacaoTurmaProvaSerapQuery : IRequest<SituacaoTurmaProvaDto>
    {
        public ObterSituacaoTurmaProvaSerapQuery(long provaId, long turmaId)
        {
            ProvaId = provaId;
            TurmaId = turmaId;
        }

        public long ProvaId { get; set; }
        public long TurmaId { get; set; }
    }
}
