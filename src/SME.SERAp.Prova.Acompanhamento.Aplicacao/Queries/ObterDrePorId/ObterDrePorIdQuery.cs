using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterDrePorIdQuery : IRequest<Dre>
    {
        public ObterDrePorIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
