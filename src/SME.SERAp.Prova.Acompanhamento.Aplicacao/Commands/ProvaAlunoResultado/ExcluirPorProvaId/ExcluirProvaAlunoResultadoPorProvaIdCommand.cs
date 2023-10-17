using FluentValidation;
using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirProvaAlunoResultadoPorProvaIdCommand : IRequest<bool>
    {
        public ExcluirProvaAlunoResultadoPorProvaIdCommand(long provaId)
        {
            ProvaId = provaId;
        }

        public long ProvaId { get; }
    }

    public class ExcluirProvaAlunoResultadoPorProvaIdCommandValidator : AbstractValidator<ExcluirProvaAlunoResultadoPorProvaIdCommand>
    {
        public ExcluirProvaAlunoResultadoPorProvaIdCommandValidator()
        {
            RuleFor(x => x.ProvaId)
                .NotEmpty()
                .WithMessage("Informe o id da prova para excluir os resultados dos alunos");
        }
    }
}
