using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaAlunoResultadoPorProvaTurmaQueryHandler : IRequestHandler<ObterProvaAlunoResultadoPorProvaTurmaQuery, IEnumerable<ProvaAlunoResultado>>
    {

        private readonly IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado;

        public ObterProvaAlunoResultadoPorProvaTurmaQueryHandler(IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado)
        {
            this.repositorioProvaAlunoResultado = repositorioProvaAlunoResultado ?? throw new ArgumentNullException(nameof(repositorioProvaAlunoResultado));
        }

        public async Task<IEnumerable<ProvaAlunoResultado>> Handle(ObterProvaAlunoResultadoPorProvaTurmaQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProvaAlunoResultado.ObterPorProvaTurmaIdAsync(request.ProvaId, request.TurmaId);
        }
    }
}
