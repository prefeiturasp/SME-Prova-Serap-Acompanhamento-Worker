﻿using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTodasProvasSerapQuery : IRequest<IEnumerable<ProvaDto>>
    {
    }
}
