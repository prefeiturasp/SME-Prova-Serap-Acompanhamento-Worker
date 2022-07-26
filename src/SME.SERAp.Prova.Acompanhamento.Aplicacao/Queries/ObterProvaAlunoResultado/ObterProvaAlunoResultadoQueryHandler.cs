﻿using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaAlunoResultadoQueryHandler : IRequestHandler<ObterProvaAlunoResultadoQuery, IEnumerable<ProvaAlunoResultado>>
    {
        private readonly IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado;

        public ObterProvaAlunoResultadoQueryHandler(IRepositorioProvaAlunoResultado repositorioProvaAlunoResultado)
        {
            this.repositorioProvaAlunoResultado = repositorioProvaAlunoResultado ?? throw new ArgumentNullException(nameof(repositorioProvaAlunoResultado));
        }

        public async Task<IEnumerable<ProvaAlunoResultado>> Handle(ObterProvaAlunoResultadoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioProvaAlunoResultado.ObterPorProvaAlunoRaAsync(request.ProvaId, request.AlunoRa);
        }
    }
}
