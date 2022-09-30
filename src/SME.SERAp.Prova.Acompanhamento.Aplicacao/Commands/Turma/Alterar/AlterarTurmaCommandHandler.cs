using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class AlterarTurmaCommandHandler : IRequestHandler<AlterarTurmaCommand, bool>
    {
        private readonly IRepositorioTurma repositorioTurma;

        public AlterarTurmaCommandHandler(IRepositorioTurma repositorioTurma)
        {
            this.repositorioTurma = repositorioTurma ?? throw new ArgumentNullException(nameof(repositorioTurma));
        }

        public async Task<bool> Handle(AlterarTurmaCommand request, CancellationToken cancellationToken)
        {
            return await repositorioTurma.AlterarAsync(new Dominio.Entities.Turma(request.TurmaDto.Id,
                    request.TurmaDto.UeId,
                    request.TurmaDto.Codigo,
                    request.TurmaDto.AnoLetivo,
                    request.TurmaDto.Ano,
                    request.TurmaDto.Nome,
                    request.TurmaDto.Modalidade,
                    request.TurmaDto.Turno,
                    request.TurmaDto.EtapaEja,
                    request.TurmaDto.SerieEnsino,
                    request.TurmaDto.Semestre
                ));
        }
    }
}
