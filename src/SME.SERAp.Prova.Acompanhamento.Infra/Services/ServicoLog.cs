﻿using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using SME.SERAp.Prova.Acompanhamento.Infra.Interfaces;
using System;
using System.Text;
using MongoDB.Bson.IO;
using SME.SERAp.Prova.Acompanhamento.Infra.Extensions;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Services
{
    public class ServicoLog : IServicoLog
    {
        private readonly ILogger<ServicoLog> logger;
        private readonly IServicoTelemetria servicoTelemetria;
        private readonly RabbitLogOptions rabbitLogOptions;

        public ServicoLog(ILogger<ServicoLog> logger, IServicoTelemetria servicoTelemetria, RabbitLogOptions rabbitLogOptions)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.servicoTelemetria = servicoTelemetria ?? throw new ArgumentNullException(nameof(servicoTelemetria));
            this.rabbitLogOptions = rabbitLogOptions ?? throw new ArgumentNullException(nameof(rabbitLogOptions));
        }

        public void Registrar(Exception ex)
        {
            LogMensagem logMensagem = new("Exception --- ", LogNivel.Critico, ex.Message, ex.StackTrace);
            Registrar(logMensagem);
        }

        public void Registrar(LogNivel nivel, string erro, string observacoes, string stackTrace)
        {
            LogMensagem logMensagem = new(erro, nivel, observacoes, stackTrace);
            Registrar(logMensagem);
        }

        public void Registrar(string mensagem, Exception ex)
        {
            LogMensagem logMensagem = new(mensagem, LogNivel.Critico, ex.Message, ex.StackTrace);
            Registrar(logMensagem);
        }

        public void Registrar(LogMensagem log)
        {
            var body = Encoding.UTF8.GetBytes(log.ConverterObjectParaJson());
            servicoTelemetria.Registrar(() => PublicarMensagem(body), "RabbitMQ", "Salvar Log Via Rabbit", RotaRabbit.Log);
        }

        private void PublicarMensagem(byte[] body)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = rabbitLogOptions.HostName,
                    UserName = rabbitLogOptions.UserName,
                    Password = rabbitLogOptions.Password,
                    VirtualHost = rabbitLogOptions.VirtualHost
                };

                using var conexaoRabbit = factory.CreateConnection();
                using var channel = conexaoRabbit.CreateModel();
                var props = channel.CreateBasicProperties();
                props.Persistent = true;
                channel.BasicPublish(ExchangeRabbit.Log, RotaRabbit.Log, props, body);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
            }
        }

        public enum LogNivel
        {
            Informacao = 1,
            Critico = 2,
            Negocio = 3
        }

        public class LogMensagem
        {
            public LogMensagem(string mensagem, LogNivel nivel, string observacao, string rastreamento = null, string excecaoInterna = null, string projeto = "SME_SERAp_Prova_Acompanhamento_Worker")
            {
                Mensagem = mensagem;
                Nivel = nivel;
                Observacao = observacao;
                Projeto = projeto;
                Rastreamento = rastreamento;
                ExcecaoInterna = excecaoInterna;
                DataHora = DateTime.Now;
            }

            public string Mensagem { get; set; }
            public LogNivel Nivel { get; set; }
            public string Observacao { get; set; }
            public string Projeto { get; set; }
            public string Rastreamento { get; set; }
            public string ExcecaoInterna { get; set; }
            public DateTime DataHora { get; set; }
        }
    }
}
