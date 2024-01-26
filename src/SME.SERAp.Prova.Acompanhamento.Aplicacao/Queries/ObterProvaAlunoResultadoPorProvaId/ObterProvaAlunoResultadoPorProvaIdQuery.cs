using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaAlunoResultadoPorProvaIdQuery : IRequest<IEnumerable<ProvaAlunoResultado>>
    {
        public ObterProvaAlunoResultadoPorProvaIdQuery(long provaId)
        {
            ProvaId = provaId;
        }

        public long ProvaId { get; }
    }

    public class ObterProvaAlunoResultadoPorProvaIdQueryValidator : AbstractValidator<ObterProvaAlunoResultadoPorProvaIdQuery>
    {
        public ObterProvaAlunoResultadoPorProvaIdQueryValidator()
        {
            RuleFor(c => c.ProvaId)
                .GreaterThan(0)
                .WithMessage("O Id da prova deve ser informado para obter os resultados dos alunos");
        }
    }
}