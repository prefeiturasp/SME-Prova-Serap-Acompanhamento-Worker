using MediatR;
using Moq;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos.SerapEstudantes;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Utilities;
using Xunit;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Teste.UseCases
{
    public class TratarProvaAlunoReaberturaUseCaseTeste
    {
        private readonly TratarProvaAlunoReaberturaUseCase useCase;
        private readonly Mock<IMediator> mediator;

        public TratarProvaAlunoReaberturaUseCaseTeste()
        {
            mediator = new Mock<IMediator>();
            useCase = new TratarProvaAlunoReaberturaUseCase(mediator.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Excecao_Quando_Mensagem_For_Invalida()
        {
            var mensagem = new MensagemRabbit(string.Empty, Guid.NewGuid());
            var excecao = await Assert.ThrowsAsync<JsonException>(() => useCase.Executar(mensagem));
            Assert.NotEmpty(excecao.Message);
        }

        [Fact]
        public async Task Deve_Publicar_Reabertura_TAI_Quando_Formato_Eh_TAI()
        {
            var dto = new ProvaAlunoReaberturaDto { ProvaId = 1 };
            var mensagem = new MensagemRabbit(JsonSerializer.Serialize(dto), Guid.NewGuid());

            mediator.Setup(m => m.Send(It.IsAny<VerificarSeProvaEhFormatoTAIQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            mediator.Setup(m => m.Send(It.IsAny<ObterProvaAlunoResultadoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ProvaAlunoResultado>());

            var resultado = await useCase.Executar(mensagem);

            mediator.Verify(m => m.Send(It.Is<PublicaFilaRabbitCommand>(c =>
                    c.NomeRota == RotaRabbit.ReabrirAlunoProvaTai), It.IsAny<CancellationToken>()), Times.Once);

            Assert.True(resultado);
        }

        [Fact]
        public async Task Nao_Deve_Publicar_Reabertura_TAI_Quando_Formato_Nao_Eh_TAI()
        {
            var dto = new ProvaAlunoReaberturaDto { ProvaId = 1 };
            var mensagem = new MensagemRabbit(JsonSerializer.Serialize(dto), Guid.NewGuid());

            mediator.Setup(m => m.Send(It.IsAny<VerificarSeProvaEhFormatoTAIQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            mediator.Setup(m => m.Send(It.IsAny<ObterProvaAlunoResultadoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ProvaAlunoResultado>());

            var resultado = await useCase.Executar(mensagem);

            mediator.Verify(m => m.Send(It.Is<PublicaFilaRabbitCommand>(c =>
                    c.NomeRota == RotaRabbit.ReabrirAlunoProvaTai), It.IsAny<CancellationToken>()), Times.Never);

            Assert.True(resultado);
        }

        [Fact]
        public async Task Deve_Retornar_True_Quando_Resultado_Null()
        {
            var dto = new ProvaAlunoReaberturaDto { ProvaId = 1 };
            var mensagem = new MensagemRabbit(JsonSerializer.Serialize(dto), Guid.NewGuid());

            mediator.Setup(m => m.Send(It.IsAny<VerificarSeProvaEhFormatoTAIQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            mediator.Setup(m => m.Send(It.IsAny<ObterProvaAlunoResultadoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<ProvaAlunoResultado>)null);

            var resultado = await useCase.Executar(mensagem);

            Assert.True(resultado);
        }

        [Fact]
        public async Task Deve_Retornar_True_Quando_Lista_Resultado_Vazia()
        {
            var dto = new ProvaAlunoReaberturaDto { ProvaId = 1 };
            var mensagem = new MensagemRabbit(JsonSerializer.Serialize(dto), Guid.NewGuid());

            mediator.Setup(m => m.Send(It.IsAny<VerificarSeProvaEhFormatoTAIQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            mediator.Setup(m => m.Send(It.IsAny<ObterProvaAlunoResultadoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ProvaAlunoResultado>());

            var resultado = await useCase.Executar(mensagem);

            Assert.True(resultado);
        }

        [Fact]
        public async Task Deve_Excluir_E_Inserir_Resultados_Quando_Resultados_Existem()
        {
            var dto = new ProvaAlunoReaberturaDto { ProvaId = 1, AlunoRa = 123, UsuarioCoresso = "teste" };
            var mensagem = new MensagemRabbit(JsonSerializer.Serialize(dto), Guid.NewGuid());
            var provaAlunoResultado = ObterProvaAlunoResultado();

            mediator.Setup(m => m.Send(It.IsAny<VerificarSeProvaEhFormatoTAIQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            mediator.Setup(m => m.Send(It.IsAny<ObterProvaAlunoResultadoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ProvaAlunoResultado> { provaAlunoResultado });

            var resultado = await useCase.Executar(mensagem);

            mediator.Verify(m => m.Send(It.IsAny<ExcluirProvaAlunoResultadoCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            mediator.Verify(m => m.Send(It.IsAny<InserirProvaAlunoResultadoCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.True(resultado);
        }

        [Fact]
        public async Task Deve_Publicar_Recalculo_Prova_Turma()
        {
            var dto = new ProvaAlunoReaberturaDto { ProvaId = 1, AlunoRa = 123, UsuarioCoresso = "user" };
            var mensagem = new MensagemRabbit(JsonSerializer.Serialize(dto), Guid.NewGuid());
            var provaAlunoResultado = ObterProvaAlunoResultado();

            mediator.Setup(m => m.Send(It.IsAny<VerificarSeProvaEhFormatoTAIQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            mediator.Setup(m => m.Send(It.IsAny<ObterProvaAlunoResultadoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ProvaAlunoResultado> { provaAlunoResultado });

            var resultado = await useCase.Executar(mensagem);

            mediator.Verify(m => m.Send(It.Is<PublicaFilaRabbitCommand>(c =>
                    c.NomeRota == RotaRabbit.ProvaTurmaResultadoRecalcular), It.IsAny<CancellationToken>()));

            Assert.True(resultado);
        }

        private static ProvaAlunoResultado ObterProvaAlunoResultado()
        {
            return new ProvaAlunoResultado
            {
                Id = Guid.NewGuid().ToString(),
                ProvaId = 1,
                DreId = 2,
                UeId = 3,
                TurmaId = 4,
                Ano = "5",
                Modalidade = Dominio.Enums.Modalidade.Fundamental,
                AnoLetivo = 2024,
                Inicio = DateTime.Now,
                Fim = DateTime.Now.AddMinutes(1),
                AlunoId = 999,
                AlunoRa = 123,
                AlunoNome = "Aluno Teste",
                AlunoNomeSocial = "Nome Social",
                AlunoDownload = true,
                AlunoTempo = 100,
                AlunoQuestaoRespondida = 5
            };
        }
    }
}
