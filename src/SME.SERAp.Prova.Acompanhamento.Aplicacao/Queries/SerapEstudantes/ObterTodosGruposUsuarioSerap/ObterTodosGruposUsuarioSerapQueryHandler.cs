using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Constraints;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTodosGruposUsuarioSerapQueryHandler : IRequestHandler<ObterTodosGruposUsuarioSerapQuery, IEnumerable<Guid>>
    {
        public async Task<IEnumerable<Guid>> Handle(ObterTodosGruposUsuarioSerapQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(Perfis.ToList());
        }
    }
}
