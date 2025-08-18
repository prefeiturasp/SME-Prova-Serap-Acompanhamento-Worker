using Moq;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Teste.Commands
{
    public class ExcluirProvaAlunoRespostaPorAlunoCommandHandlerTeste
    {
        private readonly Mock<IRepositorioProvaAlunoResposta> repositorio;
        private readonly ExcluirProvaAlunoRespostaPorAlunoCommandHandler handler;

        public ExcluirProvaAlunoRespostaPorAlunoCommandHandlerTeste()
        {
            repositorio = new Mock<IRepositorioProvaAlunoResposta>();
            handler = new ExcluirProvaAlunoRespostaPorAlunoCommandHandler(repositorio.Object);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Com_Parametros_Corretos()
        {
            var provaId = 123L;
            var alunoRa = 456L;
            var command = new ExcluirProvaAlunoRespostaPorAlunoCommand(provaId, alunoRa);

            repositorio
                .Setup(r => r.DeletarPorAlunoProvaAsync(provaId, alunoRa))
                .ReturnsAsync(true);

            var resultado = await handler.Handle(command, CancellationToken.None);

            Assert.True(resultado);
            repositorio.Verify(r => r.DeletarPorAlunoProvaAsync(provaId, alunoRa), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_False_Quando_Repositorio_Retornar_False()
        {
            var provaId = 789L;
            var alunoRa = 321L;
            var command = new ExcluirProvaAlunoRespostaPorAlunoCommand(provaId, alunoRa);

            repositorio
                .Setup(r => r.DeletarPorAlunoProvaAsync(provaId, alunoRa))
                .ReturnsAsync(false);

            var resultado = await handler.Handle(command, CancellationToken.None);

            Assert.False(resultado);
            repositorio.Verify(r => r.DeletarPorAlunoProvaAsync(provaId, alunoRa), Times.Once);
        }
    }
}
