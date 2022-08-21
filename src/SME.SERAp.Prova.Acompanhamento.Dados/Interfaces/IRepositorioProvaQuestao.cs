using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioProvaQuestao : IRepositorioBase<ProvaQuestao>
    {
        Task<IEnumerable<ProvaQuestao>> ObterPorProvaIdAsync(long provaId);
        Task<ProvaQuestao> ObterPorQuestaoIdAsync(long questaoId);
        Task<bool> ExcluirAsync(ProvaQuestao provaQuestao);
    }
}
