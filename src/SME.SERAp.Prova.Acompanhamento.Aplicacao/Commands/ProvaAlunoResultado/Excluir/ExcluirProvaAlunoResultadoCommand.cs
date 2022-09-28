using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirProvaAlunoResultadoCommand : IRequest<bool>
    {
        public ExcluirProvaAlunoResultadoCommand(string provaAlunoResultadoId)
        {
            ProvaAlunoResultadoId = provaAlunoResultadoId;
        }

        public string ProvaAlunoResultadoId { get; set; }
    }
}