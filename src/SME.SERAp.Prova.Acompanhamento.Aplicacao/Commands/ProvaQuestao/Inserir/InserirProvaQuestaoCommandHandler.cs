using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirProvaQuestaoCommandHandler : IRequestHandler<InserirProvaQuestaoCommand, bool>
    {

        private readonly IRepositorioProvaQuestao repositorioProvaQuestao;

        public InserirProvaQuestaoCommandHandler(IRepositorioProvaQuestao repositorioProvaQuestao)
        {
            this.repositorioProvaQuestao = repositorioProvaQuestao ?? throw new ArgumentNullException(nameof(repositorioProvaQuestao));
        }

        public async Task<bool> Handle(InserirProvaQuestaoCommand request, CancellationToken cancellationToken)
        {
            await repositorioProvaQuestao.CriarIndexAsync();
            return await repositorioProvaQuestao.InserirAsync(request.ProvaQuestao);
        }
    }
}
