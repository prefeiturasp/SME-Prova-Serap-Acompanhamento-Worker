using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterUePorCodigoQuery : IRequest<Ue>
    {
        public ObterUePorCodigoQuery(long codigo)
        {
            Codigo = codigo;
        }

        public long Codigo { get; set; }
    }
}
