using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.Coresso;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterGruposCoressoQuery : IRequest<IEnumerable<GrupoCoressoDto>>
    {
    }
}
