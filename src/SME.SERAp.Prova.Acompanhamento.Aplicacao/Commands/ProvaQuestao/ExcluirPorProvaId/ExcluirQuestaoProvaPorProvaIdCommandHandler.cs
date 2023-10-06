using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirQuestaoProvaPorProvaIdCommandHandler : IRequestHandler<ExcluirQuestaoProvaPorProvaIdCommand, bool>
    {
        private readonly IRepositorioProvaQuestao repositorioProvaQuestao;

        public ExcluirQuestaoProvaPorProvaIdCommandHandler(IRepositorioProvaQuestao repositorioProvaQuestao)
        {
            this.repositorioProvaQuestao = repositorioProvaQuestao ?? throw new ArgumentNullException(nameof(repositorioProvaQuestao));
        }

        public async Task<bool> Handle(ExcluirQuestaoProvaPorProvaIdCommand request, CancellationToken cancellationToken)
        {
            return await repositorioProvaQuestao.ExcluirPorProvaIdAsync(request.ProvaId);
        }
    }
}
