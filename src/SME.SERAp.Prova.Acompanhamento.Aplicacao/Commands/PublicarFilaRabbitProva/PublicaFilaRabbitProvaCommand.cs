using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Commands
{
    public class PublicaFilaRabbitProvaCommand : IRequest<bool>
    {
        public string NomeFila { get; private set; }
        public string NomeRota { get; private set; }
        public object Mensagem { get; private set; }

        public PublicaFilaRabbitProvaCommand(string nomeFila, object mensagem = null)
        {
            Mensagem = mensagem;
            NomeFila = nomeFila;
            NomeRota = nomeFila;
        }
    }
}
