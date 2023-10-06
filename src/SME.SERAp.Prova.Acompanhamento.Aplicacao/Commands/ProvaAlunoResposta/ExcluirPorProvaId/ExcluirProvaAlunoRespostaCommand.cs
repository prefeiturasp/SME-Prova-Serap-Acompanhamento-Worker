using FluentValidation;
using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirProvaAlunoRespostaPorProvaIdCommand : IRequest<bool>
    {
        public ExcluirProvaAlunoRespostaPorProvaIdCommand(long provaId)
        {
            ProvaId = provaId;
        }

        public long ProvaId { get; }
    }

    public class ExcluirProvaAlunoRespostaPorProvaIdCommandValidator : AbstractValidator<ExcluirProvaAlunoRespostaPorProvaIdCommand>
    {
        public ExcluirProvaAlunoRespostaPorProvaIdCommandValidator()
        {
            RuleFor(x => x.ProvaId)
                .NotEmpty()
                .WithMessage("Informe o id da prova para excluir as respostas dos alunos");
        }
    }
}
