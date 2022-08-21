using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaTurmaResultadoQuery : IRequest<ProvaTurmaResultado>
    {
        public ObterProvaTurmaResultadoQuery(long provaId, long turmaId)
        {
            ProvaId = provaId;
            TurmaId = turmaId;
        }

        public long ProvaId { get; set; }
        public long TurmaId { get; set; }
    }
}
