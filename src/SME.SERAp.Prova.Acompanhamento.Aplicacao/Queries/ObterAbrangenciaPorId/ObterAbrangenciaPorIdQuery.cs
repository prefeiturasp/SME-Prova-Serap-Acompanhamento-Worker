using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaPorIdQuery : IRequest<Abrangencia>
    {
        public ObterAbrangenciaPorIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
