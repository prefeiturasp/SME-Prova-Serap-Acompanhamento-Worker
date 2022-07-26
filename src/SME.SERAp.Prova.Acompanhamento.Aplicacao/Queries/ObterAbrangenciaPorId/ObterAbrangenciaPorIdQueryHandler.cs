﻿using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAbrangenciaPorIdQueryHandler : IRequestHandler<ObterAbrangenciaPorIdQuery, Abrangencia>
    {
        private readonly IRepositorioAbrangencia repositorioAbrangencia;

        public ObterAbrangenciaPorIdQueryHandler(IRepositorioAbrangencia repositorioAbrangencia)
        {
            this.repositorioAbrangencia = repositorioAbrangencia ?? throw new ArgumentNullException(nameof(repositorioAbrangencia));
        }

        public async Task<Abrangencia> Handle(ObterAbrangenciaPorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAbrangencia.ObterPorIdAsync(request.Id);
        }
    }
}
