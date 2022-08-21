using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes
{
    public interface IRepositorioSerapTurma
    {
        Task<IEnumerable<TurmaDto>> ObterPorUeIdAsync(long ueId);
        Task<IEnumerable<AlunoDto>> ObterAlunosPorIdAsync(long turmaId, DateTime provaInicio, DateTime provaFim);
    }
}
