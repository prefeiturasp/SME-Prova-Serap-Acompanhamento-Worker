using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirProvaAlunoRespostaPorAlunoCommand : IRequest<bool>
    {
        public long ProvaId { get; set; }
        public long AlunoRa { get; set; }

        public ExcluirProvaAlunoRespostaPorAlunoCommand(long provaId, long alunoRa)
        {
            ProvaId = provaId;
            AlunoRa = alunoRa;
        }
    }
}
