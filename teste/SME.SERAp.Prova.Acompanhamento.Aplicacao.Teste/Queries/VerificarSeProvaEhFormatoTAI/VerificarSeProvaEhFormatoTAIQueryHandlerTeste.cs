using Moq;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces.SerapEstudantes;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Teste.Queries
{
    public class VerificarSeProvaEhFormatoTAIQueryHandlerTeste
    {
        private readonly Mock<IRepositorioSerapProva> repositorio;
        private readonly VerificarSeProvaEhFormatoTAIQueryHandler handler;
        public VerificarSeProvaEhFormatoTAIQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioSerapProva>();
            handler = new VerificarSeProvaEhFormatoTAIQueryHandler(repositorio.Object);
        }

        [Fact]
        public async Task Deve_Retornar_True_Quando_Prova_For_Formato_TAI()
        {
            var provaId = 1;
            var query = new VerificarSeProvaEhFormatoTAIQuery(provaId);
            repositorio.Setup(r => r.VerificarSeProvaEhFormatoTAI(provaId)).ReturnsAsync(true);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.True(resultado);
            repositorio.Verify(r => r.VerificarSeProvaEhFormatoTAI(provaId), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_False_Quando_Prova_Nao_For_Formato_TAI()
        {
            var provaId = 2;
            var query = new VerificarSeProvaEhFormatoTAIQuery(provaId);
            repositorio.Setup(r => r.VerificarSeProvaEhFormatoTAI(provaId)).ReturnsAsync(false);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.False(resultado);
            repositorio.Verify(r => r.VerificarSeProvaEhFormatoTAI(provaId), Times.Once);
        }
    }
}
