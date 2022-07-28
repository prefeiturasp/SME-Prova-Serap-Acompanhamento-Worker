using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAnoPorIdQuery : IRequest<Ano>
    {
        public ObterAnoPorIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
