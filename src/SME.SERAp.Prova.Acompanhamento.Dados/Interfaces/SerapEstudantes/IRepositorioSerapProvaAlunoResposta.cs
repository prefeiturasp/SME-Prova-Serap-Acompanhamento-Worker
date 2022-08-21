using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes
{
    public interface IRepositorioSerapProvaAlunoResposta
    {
        Task<IEnumerable<ProvaAlunoRespostaDto>> ObterRespostasAsync(long provaId, long alunoRa);
    }
}
