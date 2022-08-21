using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarProvaTurmaResultadoCommand : IRequest<bool>
    {
        public AlterarProvaTurmaResultadoCommand(ProvaTurmaResultado provaTurmaResultado)
        {
            ProvaTurmaResultado = provaTurmaResultado;
        }

        public ProvaTurmaResultado ProvaTurmaResultado { get; set; }
    }
}
