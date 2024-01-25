using System.Collections.Generic;
using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvasParaDeficientesQuery : IRequest<IEnumerable<ProvaDto>>
    {
    }
}