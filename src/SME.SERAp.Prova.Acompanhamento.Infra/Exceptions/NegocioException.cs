using System;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Exceptions
{
    public class NegocioException : Exception
    {
        public NegocioException(string mensagem) : base(mensagem)
        {
        }
    }
}
