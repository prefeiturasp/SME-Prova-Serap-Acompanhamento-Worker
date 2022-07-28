using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioProvaAlunoResposta : IRepositorioBase<ProvaAlunoResposta>
    {
        Task<ProvaAlunoResposta> ObterPorProvaAlunoQuestaoAsync(long provaId, long alunoRa, long questaoId);
        Task<IEnumerable<ProvaAlunoResposta>> ObterProvaAlunoAsync(long provaId, long alunoRa);
    }
}
