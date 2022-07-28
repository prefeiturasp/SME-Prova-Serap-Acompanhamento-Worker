using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaAlunoResultadoDownloadUseCase : AbstractUseCase, ITratarProvaAlunoResultadoDownloadUseCase
    {
        public TratarProvaAlunoResultadoDownloadUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var downloadProvaAluno = mensagemRabbit.ObterObjetoMensagem<DownloadProvaAlunoDto>();
            if (downloadProvaAluno == null) return false;

            var provaTurmaAlunoSituacao = await mediator.Send(new ObterProvaTurmaAlunoResultadoQuery(downloadProvaAluno.ProvaId, downloadProvaAluno.AlunoRa));
            if (provaTurmaAlunoSituacao == null) return false;

            if (!provaTurmaAlunoSituacao.AlunoDownload)
            {
                provaTurmaAlunoSituacao.AlunoDownload = true;
                await mediator.Send(new AlterarProvaAlunoResultadoCommand(provaTurmaAlunoSituacao));
            }

            return true;
        }
    }
}
