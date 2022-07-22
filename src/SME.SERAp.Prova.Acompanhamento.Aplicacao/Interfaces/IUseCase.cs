using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces
{
    public interface IUseCase
    {
        Task<bool> Executar(MensagemRabbit mensagemRabbit);
    }
}
