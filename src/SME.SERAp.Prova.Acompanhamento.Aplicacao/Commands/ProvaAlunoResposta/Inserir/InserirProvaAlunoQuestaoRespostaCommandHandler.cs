using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirProvaAlunoQuestaoRespostaCommandHandler : IRequestHandler<InserirProvaAlunoQuestaoRespostaCommand, bool>
    {
        private readonly IRepositorioProvaAlunoResposta repositorioProvaAlunoQuestaoResposta;

        public InserirProvaAlunoQuestaoRespostaCommandHandler(IRepositorioProvaAlunoResposta repositorioProvaAlunoQuestaoResposta)
        {
            this.repositorioProvaAlunoQuestaoResposta = repositorioProvaAlunoQuestaoResposta ?? throw new ArgumentNullException(nameof(repositorioProvaAlunoQuestaoResposta));
        }

        public async Task<bool> Handle(InserirProvaAlunoQuestaoRespostaCommand request, CancellationToken cancellationToken)
        {
            return await repositorioProvaAlunoQuestaoResposta.InserirAsync(new Dominio.Entities.ProvaAlunoResposta(
                request.ProvaAlunoQuestaoRespostaDto.ProvaId,
                request.ProvaAlunoQuestaoRespostaDto.AlunoRa,
                request.ProvaAlunoQuestaoRespostaDto.QuestaoId,
                request.ProvaAlunoQuestaoRespostaDto.AlternativaId,
                request.ProvaAlunoQuestaoRespostaDto.Tempo)
                );
        }
    }
}
