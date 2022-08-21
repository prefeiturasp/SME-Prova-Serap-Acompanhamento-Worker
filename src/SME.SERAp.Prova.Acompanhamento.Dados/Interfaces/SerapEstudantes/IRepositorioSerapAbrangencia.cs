using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes
{
    public interface IRepositorioSerapAbrangencia
    {
        Task<IEnumerable<AbrangenciaDto>> ObterPorCoressoIdAsync(Guid coressoId);
    }
}
