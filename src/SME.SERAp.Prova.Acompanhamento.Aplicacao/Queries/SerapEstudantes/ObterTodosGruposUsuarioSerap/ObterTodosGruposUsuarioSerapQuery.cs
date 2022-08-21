using MediatR;
using System;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTodosGruposUsuarioSerapQuery : IRequest<IEnumerable<Guid>>
    {

    }
}
