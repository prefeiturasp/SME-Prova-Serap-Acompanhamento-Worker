using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaPorIdQuery : IRequest<Dominio.Entities.Prova>
    {
        public ObterProvaPorIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
