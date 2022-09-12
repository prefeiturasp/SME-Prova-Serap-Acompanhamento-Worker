using MediatR;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAtribuicaoTurmaPorRegistroFuncionalQuery : IRequest<IEnumerable<string>>
    {
        public ObterAtribuicaoTurmaPorRegistroFuncionalQuery(string codigo)
        {
            Codigo = codigo;
        }

        public string Codigo { get; set; }
    }
}
