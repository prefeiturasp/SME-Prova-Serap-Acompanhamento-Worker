using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterDrePorCodigoQuery : IRequest<Dre>
    {
        public ObterDrePorCodigoQuery(long codigo)
        {
            Codigo = codigo;
        }

        public long Codigo { get; set; }
    }
}
