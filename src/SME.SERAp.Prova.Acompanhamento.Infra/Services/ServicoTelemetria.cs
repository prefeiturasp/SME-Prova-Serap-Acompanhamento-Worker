using Elastic.Apm;
using Elastic.Apm.Api;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Acompanhamento.Infra.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Services
{
    public class ServicoTelemetria : IServicoTelemetria
    {
        private readonly TelemetriaOptions telemetriaOptions;

        public ServicoTelemetria(TelemetriaOptions telemetriaOptions)
        {
            this.telemetriaOptions = telemetriaOptions ?? throw new ArgumentNullException(nameof(telemetriaOptions));
        }

        public ServicoTelemetriaTransacao IniciarTransacao(string rota)
        {
            var transacao = new ServicoTelemetriaTransacao(rota);

            if (telemetriaOptions.Apm)
                transacao.TransacaoApm = Agent.Tracer?.StartTransaction(rota, "WorkerRabbitSerapAcompanhamento");

            return transacao;
        }

        public void FinalizarTransacao(ServicoTelemetriaTransacao servicoTelemetriaTransacao)
        {
            if (telemetriaOptions.Apm)
                servicoTelemetriaTransacao.TransacaoApm?.End();
        }

        public void RegistrarExcecao(ServicoTelemetriaTransacao servicoTelemetriaTransacao, Exception ex)
        {
            if (telemetriaOptions.Apm)
                servicoTelemetriaTransacao.TransacaoApm?.CaptureException(ex);
        }

        public async Task<dynamic> RegistrarComRetornoAsync<T>(Func<Task<object>> acao, string acaoNome, string telemetriaNome, string telemetriaValor)
        {
            dynamic result = default;

            if (telemetriaOptions.Apm)
            {
                Stopwatch temporizadorApm = Stopwatch.StartNew();
                result = await acao() as dynamic;
                temporizadorApm.Stop();

                Agent.Tracer.CurrentTransaction.CaptureSpan(telemetriaNome, acaoNome, (span) =>
                  {
                      span.SetLabel(telemetriaNome, telemetriaValor);
                      span.Duration = temporizadorApm.Elapsed.TotalMilliseconds;
                  });
            }
            else
            {
                result = await acao() as dynamic;
            }

            return result;
        }

        public dynamic RegistrarComRetorno<T>(Func<object> acao, string acaoNome, string telemetriaNome, string telemetriaValor)
        {
            dynamic result = default;

            if (telemetriaOptions.Apm)
            {
                Stopwatch temporizadorApm = Stopwatch.StartNew();
                result = acao();
                temporizadorApm.Stop();

                Agent.Tracer.CurrentTransaction.CaptureSpan(telemetriaNome, acaoNome, (span) =>
                {
                    span.SetLabel(telemetriaNome, telemetriaValor);
                    span.Duration = temporizadorApm.Elapsed.TotalMilliseconds;
                });
            }
            else
            {
                result = acao();
            }

            return result;
        }

        public void Registrar(Action acao, string acaoNome, string telemetriaNome, string telemetriaValor)
        {
            if (telemetriaOptions.Apm)
            {
                Stopwatch temporizadorApm = Stopwatch.StartNew();
                acao();
                temporizadorApm.Stop();

                Agent.Tracer.CurrentTransaction.CaptureSpan(telemetriaNome, acaoNome, (span) =>
                {
                    span.SetLabel(telemetriaNome, telemetriaValor);
                    span.Duration = temporizadorApm.Elapsed.TotalMilliseconds;
                });
            }
            else
            {
                acao();
            }
        }

        public async Task RegistrarAsync(Func<Task> acao, string acaoNome, string telemetriaNome, string telemetriaValor)
        {
            if (telemetriaOptions.Apm)
            {
                Stopwatch temporizadorApm = Stopwatch.StartNew();
                await acao();
                temporizadorApm.Stop();

                Agent.Tracer.CurrentTransaction.CaptureSpan(telemetriaNome, acaoNome, (span) =>
                  {
                      span.SetLabel(telemetriaNome, telemetriaValor);
                      span.Duration = temporizadorApm.Elapsed.TotalMilliseconds;
                  });
            }
            else
            {
                await acao();
            }
        }

        public class ServicoTelemetriaTransacao
        {
            public ServicoTelemetriaTransacao(string nome)
            {
                Nome = nome;
            }

            public string Nome { get; set; }
            public ITransaction TransacaoApm { get; set; }
        }
    }
}
