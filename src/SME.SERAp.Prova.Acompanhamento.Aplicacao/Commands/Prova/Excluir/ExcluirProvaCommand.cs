using FluentValidation;
using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirProvaCommand : IRequest<bool>
    {
        public ExcluirProvaCommand(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }

    public class ExcluirProvaCommandValidator : AbstractValidator<ExcluirProvaCommand>
    {
        public ExcluirProvaCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Informe o id da prova que será excluida");
        }
    }
}
