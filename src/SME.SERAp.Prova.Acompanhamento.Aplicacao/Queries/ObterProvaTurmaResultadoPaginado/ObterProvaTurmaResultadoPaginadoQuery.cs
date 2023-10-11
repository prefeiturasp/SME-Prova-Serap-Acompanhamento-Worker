using FluentValidation;
using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterProvaTurmaResultadoPaginadoQuery : IRequest<RetornoPaginadoDto<ProvaTurmaResultado>>
    {
        public ObterProvaTurmaResultadoPaginadoQuery(long provaId, string scrollId)
        {
            ProvaId = provaId;
            ScrollId = scrollId;
        }

        public long ProvaId { get; set; }
        public string ScrollId { get; }
    }

    public class ObterProvaTurmaResultadoPaginadoQueryValidator : AbstractValidator<ObterProvaTurmaResultadoPaginadoQuery>
    {
        public ObterProvaTurmaResultadoPaginadoQueryValidator()
        {
            RuleFor(x => x.ProvaId)
                .NotEmpty()
                .WithMessage("Informe o Id da prova para selecionar os resultados por turma");
        }
    }
}
