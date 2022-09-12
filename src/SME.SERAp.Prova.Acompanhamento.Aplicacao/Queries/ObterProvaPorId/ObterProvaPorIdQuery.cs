using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaPorIdQuery : IRequest<Dominio.Entities.Prova>
    {
        public ObterProvaPorIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
