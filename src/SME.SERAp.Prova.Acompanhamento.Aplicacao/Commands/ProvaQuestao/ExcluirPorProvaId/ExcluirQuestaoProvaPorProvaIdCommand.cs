using FluentValidation;
using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirQuestaoProvaPorProvaIdCommand : IRequest<bool>
    {
        public ExcluirQuestaoProvaPorProvaIdCommand(long provaId)
        {
            ProvaId = provaId;
        }

        public long ProvaId { get; }
    }

    public class ExcluirQuestaoProvaPorProvaIdCommandValidator : AbstractValidator<ExcluirQuestaoProvaPorProvaIdCommand>
    {
        public ExcluirQuestaoProvaPorProvaIdCommandValidator()
        {
            RuleFor(x => x.ProvaId)
               .NotEmpty()
               .WithMessage("Informe o id da prova para remoção das questões");
        }
    }
}
