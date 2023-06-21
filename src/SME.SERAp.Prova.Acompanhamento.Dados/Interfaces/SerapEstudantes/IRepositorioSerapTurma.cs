using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes
{
    public interface IRepositorioSerapTurma
    {
        Task<IEnumerable<TurmaDto>> ObterPorUeIdAsync(long ueId);
        Task<IEnumerable<AlunoDto>> ObterAlunosPorIdAsync(long provaId, long turmaId);
        Task<IEnumerable<AlunoDto>> ObterAlunosPorIdDeficienciaAsync(long provaId, long turmaId, long[] deficiencias);
    }
}
