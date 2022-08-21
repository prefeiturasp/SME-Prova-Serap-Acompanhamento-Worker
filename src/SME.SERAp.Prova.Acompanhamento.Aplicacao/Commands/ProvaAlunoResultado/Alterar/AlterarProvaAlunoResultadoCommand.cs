using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarProvaAlunoResultadoCommand : IRequest<bool>
    {
        public AlterarProvaAlunoResultadoCommand(Dominio.Entities.ProvaAlunoResultado provaTurmaAlunoSituacao)
        {
            ProvaTurmaAlunoSituacao = provaTurmaAlunoSituacao;
        }

        public Dominio.Entities.ProvaAlunoResultado ProvaTurmaAlunoSituacao { get; set; }
    }
}
