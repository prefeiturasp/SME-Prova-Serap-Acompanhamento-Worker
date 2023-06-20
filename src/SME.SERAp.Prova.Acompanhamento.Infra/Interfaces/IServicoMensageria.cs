using System.Threading.Tasks;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Interfaces
{
    public interface IServicoMensageria
    {
        Task<bool> Publicar(MensagemRabbit mensagemRabbit, string rota, string exchange, string nomeAcao);        
    }
}