using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes
{
    public interface IRepositorioSerapProva
    {
        Task<IEnumerable<ProvaDto>> ObterTodosAsync();

        Task<IEnumerable<ProvaTurmaDto>> ObterTurmasAsync(long provaId);
        Task<SituacaoAlunoProvaDto> ObterSituacaoAlunoAsync(long provaId, long ra);
        Task<SituacaoTurmaProvaDto> ObterSituacaoTurmaAsync(long provaId, long turmaId);
        Task<ProvaTurmaDto> ObterTurmaAsync(long provaId, long turmaId);
        Task<IEnumerable<ProvaDto>> ObterProvasEmAndamentoAsync();
        Task<IEnumerable<long>> ObterDeficienciasAsync(long provaId);
    }
}
