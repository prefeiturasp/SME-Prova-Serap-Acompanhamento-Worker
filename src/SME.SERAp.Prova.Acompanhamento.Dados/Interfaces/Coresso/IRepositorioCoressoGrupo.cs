using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.Coresso
{
    public interface IRepositorioCoressoGrupo
    {
        Task<IEnumerable<GrupoCoressoDto>> ObterGruposPorSistemaModulo(long sistemaId, long moduloId);
    }
}
