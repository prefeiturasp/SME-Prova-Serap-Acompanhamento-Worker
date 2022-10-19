using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirAbrangenciaCommand : IRequest<bool>
    {
        public ExcluirAbrangenciaCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
