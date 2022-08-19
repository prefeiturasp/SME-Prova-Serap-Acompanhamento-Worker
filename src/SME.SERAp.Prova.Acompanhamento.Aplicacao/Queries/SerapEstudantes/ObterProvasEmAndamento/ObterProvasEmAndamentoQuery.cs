using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvasEmAndamentoQuery : IRequest<IEnumerable<ProvaDto>>
    {
    }
}
