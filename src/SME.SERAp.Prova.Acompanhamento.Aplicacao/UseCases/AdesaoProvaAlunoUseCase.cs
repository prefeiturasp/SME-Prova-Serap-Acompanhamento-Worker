using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class AdesaoProvaAlunoUseCase : AbstractUseCase, IAdesaoProvaAlunoUseCase
    {
        public AdesaoProvaAlunoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            long provaId = long.Parse(mensagemRabbit.Mensagem.ToString());

            RetornoPaginadoDto<ProvaAlunoResultado> provaAlunoResultadoPaginadoDto;
            string scrollId = string.Empty;

            var provaTurmasSerap = await mediator.Send(new ObterProvasTurmasSerapQuery(provaId));
            foreach (var provaTurmaSerap in provaTurmasSerap)
            {
                IEnumerable<long> deficienciasSerap = Enumerable.Empty<long>();
                if (provaTurmaSerap.Deficiente)
                    deficienciasSerap = await mediator.Send(new ObterDeficienciasPorProvaIdQuery(provaTurmaSerap.ProvaId));

                var alunosSerap = await mediator.Send(new ObterAlunosTurmaSerapQuery(provaTurmaSerap.ProvaId, provaTurmaSerap.TurmaId, provaTurmaSerap.Deficiente, deficienciasSerap.ToArray()));
                do
                {
                    provaAlunoResultadoPaginadoDto = await mediator.Send(new ObterProvaAlunoResultadoPaginadoQuery(provaTurmaSerap.ProvaId, provaTurmaSerap.TurmaId, scrollId));

                    if (provaAlunoResultadoPaginadoDto != null)
                    {
                        scrollId = provaAlunoResultadoPaginadoDto.ScrollId;

                        if (provaAlunoResultadoPaginadoDto.Items.Any())
                        {
                            foreach (var provaAlunoResultado in provaAlunoResultadoPaginadoDto.Items)
                            {
                                if (!alunosSerap.Any(t => t.Ra == provaAlunoResultado.AlunoRa) && provaAlunoResultado.AlunoSituacao != 99)
                                {
                                    provaAlunoResultado.AlunoSituacao = 99;
                                    await mediator.Send(new AlterarProvaAlunoResultadoCommand(provaAlunoResultado));
                                }
                            }
                        }
                    }

                } while (provaAlunoResultadoPaginadoDto != null && provaAlunoResultadoPaginadoDto.Items.Any());
            }

            return true;
        }
    }
}
