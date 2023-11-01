using System.Collections.Generic;
using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTurmasQuery : IRequest<IEnumerable<Turma>>
    {
        
    }
}