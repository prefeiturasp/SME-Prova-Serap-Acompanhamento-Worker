using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Dtos
{
    public class RetornoPaginadoDto<T>
    {
        public string ScrollId { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
