using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ExcluirProvaQuestaoCommandHandler : IRequestHandler<ExcluirProvaQuestaoCommand, bool>
    {

        private readonly IRepositorioProvaQuestao repositorioProvaQuestao;

        public ExcluirProvaQuestaoCommandHandler(IRepositorioProvaQuestao repositorioProvaQuestao)
        {
            this.repositorioProvaQuestao = repositorioProvaQuestao ?? throw new ArgumentNullException(nameof(repositorioProvaQuestao));
        }

        public async Task<bool> Handle(ExcluirProvaQuestaoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioProvaQuestao.ExcluirAsync(request.ProvaQuestao);
        }
    }
}
