using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class AdesaoProvaTurmaUseCase : AbstractUseCase, IAdesaoProvaTurmaUseCase
    {
        public AdesaoProvaTurmaUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            long provaId = long.Parse(mensagemRabbit.Mensagem.ToString());

            var provaTurmasSerap = await mediator.Send(new ObterProvasTurmasSerapQuery(provaId));
            RetornoPaginadoDto<ProvaTurmaResultado> provaTurmaResultadoPaginadoDTO;
            string scrollId = string.Empty;
            do
            {
                provaTurmaResultadoPaginadoDTO = await mediator.Send(new ObterProvaTurmaResultadoPaginadoQuery(provaId, scrollId));
                if (provaTurmaResultadoPaginadoDTO != null)
                {
                    scrollId = provaTurmaResultadoPaginadoDTO.ScrollId;

                    if (provaTurmaResultadoPaginadoDTO.Items.Any())
                    {
                        foreach (var provaTurmaResultado in provaTurmaResultadoPaginadoDTO.Items)
                        {
                            if (!provaTurmasSerap.Any(t => t.TurmaId == provaTurmaResultado.TurmaId) && provaTurmaResultado.TotalAlunos > 0)
                            {
                                provaTurmaResultado.Inicio = DateTime.MinValue;
                                provaTurmaResultado.Fim = DateTime.MinValue;
                                provaTurmaResultado.TotalAlunos = 0;
                                provaTurmaResultado.TotalIniciadas = 0;
                                provaTurmaResultado.TotalNaoFinalizados = 0;
                                provaTurmaResultado.TotalFinalizados = 0;
                                provaTurmaResultado.QuantidadeQuestoes = 0;
                                provaTurmaResultado.TotalQuestoes = 0;
                                provaTurmaResultado.QuestoesRespondidas = 0;
                                provaTurmaResultado.TempoMedio = 0;

                                await mediator.Send(new AlterarProvaTurmaResultadoCommand(provaTurmaResultado));
                            }
                        }
                    }
                }
            } while (provaTurmaResultadoPaginadoDTO != null && provaTurmaResultadoPaginadoDTO.Items.Any());

            return true;
        }
    }
}
