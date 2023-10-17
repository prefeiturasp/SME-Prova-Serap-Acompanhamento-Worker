using FluentValidation;
using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirProvaTurmaResultadoPorProvaIdCommand : IRequest<bool>
    {
        public ExcluirProvaTurmaResultadoPorProvaIdCommand(long provaId)
        {
            ProvaId = provaId;
        }

        public long ProvaId { get; }
    }

    public class ExcluirProvaTurmaResultadoPorProvaIdCommandValidator : AbstractValidator<ExcluirProvaTurmaResultadoPorProvaIdCommand>
    {
        public ExcluirProvaTurmaResultadoPorProvaIdCommandValidator()
        {
            RuleFor(x => x.ProvaId)
                .NotEmpty()
                .WithMessage("Informe o id da prova para excluir o resultado das turmas");
        }
    }
}
