using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Interfaces
{
    public interface IServicoMensageria
    {
        Task<bool> Publicar(MensagemRabbit mensagemRabbit, string rota, string exchange, string nomeAcao);
    }
}