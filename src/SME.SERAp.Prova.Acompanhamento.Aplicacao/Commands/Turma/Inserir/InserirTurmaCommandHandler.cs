using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class InserirTurmaCommandHandler : IRequestHandler<InserirTurmaCommand, bool>
    {
        private readonly IRepositorioTurma repositorioTurma;

        public InserirTurmaCommandHandler(IRepositorioTurma repositorioTurma)
        {
            this.repositorioTurma = repositorioTurma ?? throw new ArgumentNullException(nameof(repositorioTurma));
        }

        public async Task<bool> Handle(InserirTurmaCommand request, CancellationToken cancellationToken)
        {
            return await repositorioTurma.InserirAsync(new Dominio.Entities.Turma(request.TurmaDto.Id,
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
