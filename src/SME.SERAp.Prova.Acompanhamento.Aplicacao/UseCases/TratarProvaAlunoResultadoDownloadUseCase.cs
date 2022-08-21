using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Linq;
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

            var provaAlunoResultados = await mediator.Send(new ObterProvaAlunoResultadoQuery(downloadProvaAluno.ProvaId, downloadProvaAluno.AlunoRa));
            if (provaAlunoResultados == null || !provaAlunoResultados.Any()) return false;

            foreach (var resultado in provaAlunoResultados)
            {
                if (!resultado.AlunoDownload)
                {
                    resultado.AlunoDownload = true;
                    await mediator.Send(new AlterarProvaAlunoResultadoCommand(resultado));
                }
            }

            return true;
        }
    }
}
