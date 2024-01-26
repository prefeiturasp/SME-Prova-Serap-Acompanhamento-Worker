using FluentValidation;
using MediatR;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaParaDeficientePorProvaIdQuery : IRequest<ProvaDto>
    {
        public ObterProvaParaDeficientePorProvaIdQuery(long provaId)
        {
            ProvaId = provaId;
        }

        public long ProvaId { get; }
    }

    public class ObterProvaParaDeficientePorProvaIdQueryValidator : AbstractValidator<ObterProvaParaDeficientePorProvaIdQuery>
    {
        public ObterProvaParaDeficientePorProvaIdQueryValidator()
        {
            RuleFor(c => c.ProvaId)
                .GreaterThan(0)
                .WithMessage("O Id da prova deve ser informado para obter a prova para deficiente.");
        }
    }
}