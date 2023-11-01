using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaAlunoResultadoPorTurmaIdQuery : IRequest<IEnumerable<ProvaAlunoResultado>>
    {
        public ObterProvaAlunoResultadoPorTurmaIdQuery(long turmaId)
        {
            TurmaId = turmaId;
        }

        public long TurmaId { get; }
    }

    public class ObterProvaAlunoResultadoPorTurmaIdValidator : AbstractValidator<ObterProvaAlunoResultadoPorTurmaIdQuery>
    {
        public ObterProvaAlunoResultadoPorTurmaIdValidator()
        {
            RuleFor(c => c.TurmaId)
                .GreaterThan(0)
                .WithMessage("O id da turma deve ser informado para obter o resultados das provas dos alunos da turma.");
        }
    }
}