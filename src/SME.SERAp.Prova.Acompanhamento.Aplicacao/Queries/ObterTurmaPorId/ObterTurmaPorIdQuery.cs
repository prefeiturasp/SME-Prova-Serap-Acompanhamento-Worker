using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTurmaPorIdQuery : IRequest<Turma>
    {
        public ObterTurmaPorIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
