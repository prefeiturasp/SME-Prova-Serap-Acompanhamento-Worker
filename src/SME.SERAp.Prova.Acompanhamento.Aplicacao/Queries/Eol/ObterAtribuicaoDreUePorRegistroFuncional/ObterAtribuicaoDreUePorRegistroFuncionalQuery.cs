using MediatR;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAtribuicaoDreUePorRegistroFuncionalQuery : IRequest<IEnumerable<string>>
    {
        public ObterAtribuicaoDreUePorRegistroFuncionalQuery(string codigo)
        {
            Codigo = codigo;
        }

        public string Codigo { get; set; }
    }
}
