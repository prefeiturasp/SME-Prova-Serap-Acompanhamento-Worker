﻿using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.Eol;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAtribuicaoTurmaPorRegistroFuncionalQueryHandler : IRequestHandler<ObterAtribuicaoTurmaPorRegistroFuncionalQuery, IEnumerable<string>>
    {
        private readonly EolOptions eolOptions;
        private readonly IRepositorioEolAtribuicao repositorioEolAtribuicao;

        public ObterAtribuicaoTurmaPorRegistroFuncionalQueryHandler(EolOptions eolOptions, IRepositorioEolAtribuicao repositorioEolAtribuicao)
        {
            this.eolOptions = eolOptions ?? throw new ArgumentNullException(nameof(eolOptions));
            this.repositorioEolAtribuicao = repositorioEolAtribuicao ?? throw new ArgumentNullException(nameof(repositorioEolAtribuicao));
        }

        public async Task<IEnumerable<string>> Handle(ObterAtribuicaoTurmaPorRegistroFuncionalQuery request, CancellationToken cancellationToken)
        {
            var tipos = eolOptions.TiposEscola
                .Split(',')
                .Select(t => int.Parse(t))
                .ToArray();

            return await repositorioEolAtribuicao.ObterAtribuicaoTurmaPorRegistroFuncional(tipos, request.Codigo);
        }
    }
}
