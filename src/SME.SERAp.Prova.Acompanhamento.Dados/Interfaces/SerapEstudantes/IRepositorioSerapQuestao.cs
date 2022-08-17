using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes
{
    public interface IRepositorioSerapQuestao
    {
        Task<IEnumerable<long>> ObterQuestoesIdsPorProvaId(long provaId);
    }
}
