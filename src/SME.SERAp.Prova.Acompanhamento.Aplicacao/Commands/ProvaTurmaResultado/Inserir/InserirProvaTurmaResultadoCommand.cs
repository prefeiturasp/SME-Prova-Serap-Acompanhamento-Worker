using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirProvaTurmaResultadoCommand : IRequest<bool>
    {
        public InserirProvaTurmaResultadoCommand(ProvaTurmaResultado provaTurmaResultado)
        {
            ProvaTurmaResultado = provaTurmaResultado;
        }

        public ProvaTurmaResultado ProvaTurmaResultado { get; set; }
    }
}
