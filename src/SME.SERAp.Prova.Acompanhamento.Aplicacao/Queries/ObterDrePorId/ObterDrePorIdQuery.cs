using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterDrePorIdQuery : IRequest<Dre>
    {
        public ObterDrePorIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
