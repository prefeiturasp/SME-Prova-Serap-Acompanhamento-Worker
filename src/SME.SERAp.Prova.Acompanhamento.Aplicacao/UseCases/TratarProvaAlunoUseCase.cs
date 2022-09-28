using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries.SerapEstudantes.ObterSituacaoTurmaProvaSerap;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaAlunoUseCase : AbstractUseCase, ITratarProvaAlunoUseCase
    {
        public TratarProvaAlunoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provaTurma = mensagemRabbit.ObterObjetoMensagem<ProvaTurmaDto>();
            if (provaTurma == null) return false;

            var alunos = await mediator.Send(new ObterAlunosTurmaSerapQuery(provaTurma.TurmaId, provaTurma.Inicio, provaTurma.Fim));
            if (alunos != null && alunos.Any())
            {
                int tempoTotal = 0;
                int totalQuestoesRespondidas = 0;
                foreach (var aluno in alunos)
                {
                    try
                    { 
                        var situacaoAlunoProva = await mediator.Send(new ObterSituacaoAlunoProvaSerapQuery(provaTurma.ProvaId, aluno.Ra));
                        
                        var provaAlunoResultado = new ProvaAlunoResultado(
                            provaTurma.ProvaId,
                            provaTurma.DreId,
                            provaTurma.UeId,
                            provaTurma.TurmaId,
                            provaTurma.Ano,
                            provaTurma.Modalidade,
                            provaTurma.AnoLetivo,
                            provaTurma.Inicio,
                            provaTurma.Fim,
                            aluno.Id,
                            aluno.Ra,
                            aluno.Nome,
                            aluno.NomeSocial,
                            aluno.Situacao,
                            situacaoAlunoProva?.FezDownload ?? false,
                            situacaoAlunoProva?.Inicio,
                            situacaoAlunoProva?.Fim,
                            situacaoAlunoProva?.Tempo,
                            situacaoAlunoProva?.QuestaoRespondida,
                            situacaoAlunoProva?.UsuarioIdReabertura,
                            situacaoAlunoProva?.DataHoraReabertura,
                            null
                            
                            
                               );

                        await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaAlunoResultadoTratar, provaAlunoResultado));
                        await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaAlunoRespostaSync, new { provaTurma.ProvaId, AlunoRa = aluno.Ra }));

                        totalQuestoesRespondidas += provaAlunoResultado.AlunoQuestaoRespondida.GetValueOrDefault();
                        tempoTotal += situacaoAlunoProva?.Fim is not null ? provaAlunoResultado.AlunoTempo.GetValueOrDefault() : 0;
                    }
                    catch(Exception ex)
                    {
                        var mensagem = ex.Message;
                    }
                }

                var totalAlunos = alunos.Where(t => t.Situacao != 99).Count();
                var totalQuestoes = totalAlunos * provaTurma.QuantidadeQuestoes;

                var situacaoTurmaProva = await mediator.Send(new ObterSituacaoTurmaProvaSerapQuery(provaTurma.ProvaId, provaTurma.TurmaId));

                var provaTurmaResultado = new ProvaTurmaResultado(
                        provaTurma.ProvaId,
                        provaTurma.DreId,
                        provaTurma.UeId,
                        provaTurma.TurmaId,
                        provaTurma.Ano,
                        provaTurma.Modalidade,
                        provaTurma.AnoLetivo,
                        provaTurma.Inicio,
                        provaTurma.Fim,
                        provaTurma.Descricao,
                        totalAlunos,
                        situacaoTurmaProva.TotalIniciadoHoje,
                        situacaoTurmaProva.TotalIniciadoNaoFinalizado,
                        situacaoTurmaProva.TotalFinalizado,
                        provaTurma.QuantidadeQuestoes,
                        totalQuestoes,
                        totalQuestoesRespondidas,
                        tempoTotal > 0 ? (tempoTotal / 60) / situacaoTurmaProva.TotalFinalizado : 0
                       );

                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaTurmaResultadoTratar, provaTurmaResultado));
            }

            return true;
        }
    }
}
