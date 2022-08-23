using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirProvaTurmaResultadoCommand : IRequest<bool>
    {
        public ExcluirProvaTurmaResultadoCommand(ProvaTurmaResultado provaTurmaResultado)
        {
            ProvaTurmaResultado = provaTurmaResultado;
        }

        public ProvaTurmaResultado ProvaTurmaResultado { get; set; }
    }
}
