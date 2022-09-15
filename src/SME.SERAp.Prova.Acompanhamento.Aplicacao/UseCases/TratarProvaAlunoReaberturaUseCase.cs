using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class TratarProvaAlunoReaberturaUseCase : AbstractUseCase, ITratarProvaAlunoReaberturaUseCase
    {
        public TratarProvaAlunoReaberturaUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var provaAlunoReabertura = mensagemRabbit.ObterObjetoMensagem<ProvaAlunoReaberturaDto>();
            if (provaAlunoReabertura == null) return false; // Colocar o LOG do service log caso a msg seja vazia

            var provaAlunoResultados = await mediator.Send(new ObterProvaAlunoResultadoQuery(provaAlunoReabertura.ProvaId, provaAlunoReabertura.AlunoRa));
            if (provaAlunoResultados == null || !provaAlunoResultados.Any()) return false;

            foreach (var resultado in provaAlunoResultados)
            {

                resultado.AlunoInicio = null;
                resultado.AlunoFim = null;
                resultado.Situacao = SituacaoProvaAluno.NaoIniciado;

                await mediator.Send(new AlterarProvaAlunoResultadoCommand(resultado));
            }

            var provaAlunoResultado = provaAlunoResultados.FirstOrDefault();
            var provaTurmaRecalcular = new ProvaTurmaRecalcularDto(provaAlunoResultado.ProvaId, provaAlunoResultado.TurmaId);
            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ProvaTurmaResultadoRecalcular, provaTurmaRecalcular));

            return true;
        }
    }
}