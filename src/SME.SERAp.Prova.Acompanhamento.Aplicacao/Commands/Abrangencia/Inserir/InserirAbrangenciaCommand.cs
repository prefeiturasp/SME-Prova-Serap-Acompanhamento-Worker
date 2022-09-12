using MediatR;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirAbrangenciaCommand : IRequest<bool>
    {
        public InserirAbrangenciaCommand(Dominio.Entities.Abrangencia abrangencia)
        {
            Abrangencia = abrangencia;
        }

        public Dominio.Entities.Abrangencia Abrangencia { get; set; }
    }
}
