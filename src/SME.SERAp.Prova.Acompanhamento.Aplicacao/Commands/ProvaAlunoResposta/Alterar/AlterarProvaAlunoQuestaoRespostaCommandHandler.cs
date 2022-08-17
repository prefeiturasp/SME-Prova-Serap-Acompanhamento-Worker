using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarProvaAlunoQuestaoRespostaCommandHandler : IRequestHandler<AlterarProvaAlunoQuestaoRespostaCommand, bool>
    {
        private readonly IRepositorioProvaAlunoResposta repositorioProvaAlunoQuestaoResposta;

        public AlterarProvaAlunoQuestaoRespostaCommandHandler(IRepositorioProvaAlunoResposta repositorioProvaAlunoQuestaoResposta)
        {
            this.repositorioProvaAlunoQuestaoResposta = repositorioProvaAlunoQuestaoResposta ?? throw new ArgumentNullException(nameof(repositorioProvaAlunoQuestaoResposta));
        }

        public async Task<bool> Handle(AlterarProvaAlunoQuestaoRespostaCommand request, CancellationToken cancellationToken)
        {
            var dto = new Dominio.Entities.ProvaAlunoResposta(
               request.ProvaAlunoQuestaoRespostaDto.ProvaId,
               request.ProvaAlunoQuestaoRespostaDto.AlunoRa,
               request.ProvaAlunoQuestaoRespostaDto.QuestaoId,
               request.ProvaAlunoQuestaoRespostaDto.AlternativaId,
               request.ProvaAlunoQuestaoRespostaDto.Tempo);
            dto.Id = request.IdIndex;

            return await repositorioProvaAlunoQuestaoResposta.AlterarAsync(dto);
        }
    }
}
