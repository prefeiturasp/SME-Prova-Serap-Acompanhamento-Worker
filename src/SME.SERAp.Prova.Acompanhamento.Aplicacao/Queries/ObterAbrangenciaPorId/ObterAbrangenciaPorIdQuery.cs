using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaPorIdQuery : IRequest<Abrangencia>
    {
        public ObterAbrangenciaPorIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
