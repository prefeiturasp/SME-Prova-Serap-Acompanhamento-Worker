using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.Eol
{
    public interface IRepositorioEolAtribuicao
    {
        Task<IEnumerable<string>> ObterAtribuicaoDreUePorRegistroFuncional(int[] tiposEscola, string codigoRf);
        Task<IEnumerable<string>> ObterAtribuicaoTurmaPorRegistroFuncional(int[] tiposEscola, string codigoRf);
    }
}
