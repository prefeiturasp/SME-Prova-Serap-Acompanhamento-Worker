using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaAlunoResultadoPorProvaIdQueryHandler : IRequestHandler<ObterProvaAlunoResultadoPorProvaIdQuery, IEnumerable<ProvaAlunoResultado>>
    {
        private readonly IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado;

        public ObterProvaAlunoResultadoPorProvaIdQueryHandler(IRepositorioProvaAlunoResultado provaAlunoResultado)
        {
            repositorioProvaAlunoResultado = provaAlunoResultado ?? throw new ArgumentNullException(nameof(provaAlunoResultado));
        }

        public async Task<IEnumerable<ProvaAlunoResultado>> Handle(ObterProvaAlunoResultadoPorProvaIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProvaAlunoResultado.ObterPorProvaIdAsync(request.ProvaId);
        }
    }
}