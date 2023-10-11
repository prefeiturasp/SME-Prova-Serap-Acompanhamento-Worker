using FluentValidation;
using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaAlunoResultadoPaginadoQuery : IRequest<RetornoPaginadoDto<ProvaAlunoResultado>>
    {
        public ObterProvaAlunoResultadoPaginadoQuery(long provaId, long turmaId, string scrollId)
        {
            ProvaId = provaId;
            TurmaId = turmaId;
            ScrollId = scrollId;
        }

        public long ProvaId { get; }
        public long TurmaId { get; }
        public string ScrollId { get; }
    }

    public class ObterProvaAlunoResultadoPaginadoQueryValidator : AbstractValidator<ObterProvaAlunoResultadoPaginadoQuery>
    {
        public ObterProvaAlunoResultadoPaginadoQueryValidator()
        {
            RuleFor(x => x.ProvaId)
                .NotEmpty()
                .WithMessage("Informe o Id da prova para selecionar os resultados por aluno");

            RuleFor(x => x.TurmaId)
                .NotEmpty()
                .WithMessage("Informe o Id da turma para selecionar os resultados por aluno");
        }
    }
}
