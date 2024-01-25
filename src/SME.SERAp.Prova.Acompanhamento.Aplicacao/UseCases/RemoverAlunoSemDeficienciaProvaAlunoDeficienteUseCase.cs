using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class RemoverAlunoSemDeficienciaProvaAlunoDeficienteUseCase : AbstractUseCase, IRemoverAlunoSemDeficienciaProvaAlunoDeficienteUseCase
    {
        public RemoverAlunoSemDeficienciaProvaAlunoDeficienteUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var mensagem = mensagemRabbit.Mensagem.ToString();
            if (mensagem == null)
                return false;
            
            var provaId = long.Parse(mensagem);
            if (provaId <= 0)
                return false;

            var prova = await mediator.Send(new ObterProvaParaDeficientePorProvaIdQuery(provaId));
            if (prova == null)
                return false;
            
            var provasAlunosResultados = await mediator.Send(new ObterProvaAlunoResultadoPorProvaIdQuery(provaId));
            if (provasAlunosResultados == null || !provasAlunosResultados.Any())
                return false;
            
            var deficiencias = await mediator.Send(new ObterDeficienciasPorProvaIdQuery(provaId));
            if (deficiencias == null || !deficiencias.Any())
                return false;
            
            int totalAlunos;
            int totalNaoFinalizados;
            int totalFinalizados;
            int questoesRespondidas;
            int tempoTotal;  

            var alunos = new List<long>();
            var quantidadeQuestoes = prova.QuantidadeQuestoes;            

            var turmasIds = provasAlunosResultados.Select(c => c.TurmaId).Distinct(); 
            foreach (var turmaId in turmasIds)
            {
                totalAlunos = 0;
                totalNaoFinalizados = 0;
                totalFinalizados = 0;
                questoesRespondidas = 0;
                tempoTotal = 0;                
                
                var alunosComDeficiencias = await mediator.Send(new ObterAlunosTurmaSerapQuery(provaId, turmaId, true, deficiencias.ToArray()));
                
                Ue ue = null;
                var turma = await mediator.Send(new ObterTurmaPorIdQuery(turmaId.ToString()));
                if (turma != null)
                    ue = await mediator.Send(new ObterUePorIdQuery(turma.UeId.ToString()));

                foreach (var provaAlunoResultadoTurma in provasAlunosResultados.Where(c => c.TurmaId == turmaId))
                {
                    if ((provaAlunoResultadoTurma.AlunoQuestaoRespondida != null ||
                            (alunosComDeficiencias.Select(c => c.Ra).Contains(provaAlunoResultadoTurma.AlunoRa) && provaAlunoResultadoTurma.AlunoSituacao != 99)) &&
                        !alunos.Contains(provaAlunoResultadoTurma.AlunoRa))
                    {
                        alunos.Add(provaAlunoResultadoTurma.AlunoRa);
                        
                        questoesRespondidas += provaAlunoResultadoTurma.AlunoQuestaoRespondida.GetValueOrDefault();
                        totalAlunos++;

                        if (provaAlunoResultadoTurma.AlunoInicio != null &&
                            provaAlunoResultadoTurma.AlunoInicio.Value.Date < DateTime.Now.Date &&
                            provaAlunoResultadoTurma.AlunoFim == null)
                        {
                            totalNaoFinalizados++;
                        }

                        if (provaAlunoResultadoTurma.AlunoInicio != null &&
                            provaAlunoResultadoTurma.AlunoFim != null)
                        {
                            totalFinalizados++;
                        }

                        if (provaAlunoResultadoTurma.AlunoFim != null && provaAlunoResultadoTurma.AlunoTempo > 0)
                            tempoTotal += provaAlunoResultadoTurma.AlunoTempo.GetValueOrDefault();
                        
                        //-> Atualizar a DRE. Houve casos que a DRE estava incorreta
                        if (ue != null)
                        {
                            if (provaAlunoResultadoTurma.DreId != ue.DreId)
                            {
                                provaAlunoResultadoTurma.DreId = ue.DreId;
                                await mediator.Send(new AlterarProvaAlunoResultadoCommand(provaAlunoResultadoTurma));
                            }
                        }

                        continue;
                    }

                    provaAlunoResultadoTurma.InutilizarRegistro();
                    await mediator.Send(new AlterarProvaAlunoResultadoCommand(provaAlunoResultadoTurma));
                }

                var provaTurmaResultado = await mediator.Send(new ObterProvaTurmaResultadoQuery(provaId, turmaId));

                //-> Atualizar a DRE. Houve casos que a DRE estava incorreta
                if (ue != null)
                {
                    if (provaTurmaResultado.DreId != ue.DreId)
                    {
                        provaTurmaResultado.DreId = ue.DreId;
                        await mediator.Send(new AlterarProvaTurmaResultadoCommand(provaTurmaResultado));
                    }
                }
                
                var totalQuestoes = totalAlunos * quantidadeQuestoes;
                var tempoMedio = UtilTempoMedio.CalcularTempoMedioEmMinutos(tempoTotal, totalFinalizados);

                provaTurmaResultado.TotalAlunos = totalAlunos;
                provaTurmaResultado.TotalQuestoes = totalQuestoes;
                provaTurmaResultado.TotalNaoFinalizados = totalNaoFinalizados;
                provaTurmaResultado.TotalFinalizados = totalFinalizados;
                provaTurmaResultado.QuestoesRespondidas = questoesRespondidas;
                provaTurmaResultado.QuantidadeQuestoes = quantidadeQuestoes;
                provaTurmaResultado.TempoMedio = tempoMedio;
                await mediator.Send(new AlterarProvaTurmaResultadoCommand(provaTurmaResultado));
            }

            return true;
        }
    }
}