using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirProvaAlunoResultadoCommand : IRequest<bool>
    {
        public InserirProvaAlunoResultadoCommand(Dominio.Entities.ProvaAlunoResultado provaTurmaAlunoSituacao)
        {
            ProvaTurmaAlunoSituacao = provaTurmaAlunoSituacao;
        }

        public Dominio.Entities.ProvaAlunoResultado ProvaTurmaAlunoSituacao { get; set; }
    }
}
