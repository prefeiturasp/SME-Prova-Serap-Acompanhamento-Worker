using SME.SERAp.Prova.Acompanhamento.Infra.Services;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Interfaces
{
    public interface IServicoTelemetria
    {
        void FinalizarTransacao(ServicoTelemetria.ServicoTelemetriaTransacao servicoTelemetriaTransacao);
        ServicoTelemetria.ServicoTelemetriaTransacao IniciarTransacao(string rota);
        void Registrar(Action acao, string acaoNome, string telemetriaNome, string telemetriaValor);
        Task RegistrarAsync(Func<Task> acao, string acaoNome, string telemetriaNome, string telemetriaValor);
        dynamic RegistrarComRetorno<T>(Func<object> acao, string acaoNome, string telemetriaNome, string telemetriaValor);
        Task<dynamic> RegistrarComRetornoAsync<T>(Func<Task<object>> acao, string acaoNome, string telemetriaNome, string telemetriaValor);
        void RegistrarExcecao(ServicoTelemetria.ServicoTelemetriaTransacao servicoTelemetriaTransacao, Exception ex);
    }
}
