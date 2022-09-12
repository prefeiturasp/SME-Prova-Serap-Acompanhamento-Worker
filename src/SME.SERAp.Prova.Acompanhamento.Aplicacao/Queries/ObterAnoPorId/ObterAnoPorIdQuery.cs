using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAnoPorIdQuery : IRequest<Ano>
    {
        public ObterAnoPorIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
