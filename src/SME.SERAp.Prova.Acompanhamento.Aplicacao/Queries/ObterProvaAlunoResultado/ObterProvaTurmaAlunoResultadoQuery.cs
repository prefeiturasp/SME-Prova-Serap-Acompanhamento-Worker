using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaTurmaAlunoResultadoQuery : IRequest<ProvaAlunoResultado>
    {
        public ObterProvaTurmaAlunoResultadoQuery(long provaId, long alunoRa)
        {
            ProvaId = provaId;
            AlunoRa = alunoRa;
        }

        public long ProvaId { get; set; }
        public long AlunoRa { get; set; }
    }
}
