using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTurmaPorIdQuery : IRequest<Turma>
    {
        public ObterTurmaPorIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
