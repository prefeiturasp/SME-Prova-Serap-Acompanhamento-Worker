using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarAbrangenciaCommand : IRequest<bool>
    {
        public AlterarAbrangenciaCommand(Dominio.Entities.Abrangencia abrangencia)
        {
            Abrangencia = abrangencia;
        }

        public Dominio.Entities.Abrangencia Abrangencia { get; set; }
    }
}
