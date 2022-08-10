using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterSituacaoAlunoProvaSerapQuery : IRequest<SituacaoAlunoProvaDto>
    {
        public ObterSituacaoAlunoProvaSerapQuery(long provaId, long ra)
        {
            ProvaId = provaId;
            Ra = ra;
        }

        public long ProvaId { get; set; }
        public long Ra { get; set; }
    }
}
