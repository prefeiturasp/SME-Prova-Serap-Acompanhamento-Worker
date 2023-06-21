﻿using Elastic.Apm.AspNetCore;
using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.SqlClient;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nest;
using RabbitMQ.Client;
using SME.SERAp.Prova.Acompanhamento.Dados;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Acompanhamento.Infra.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Services;
using SME.SERAp.Prova.Acompanhamento.IoC;
using System;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Worker
{
    internal class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigEnvoiromentVariables(services);
            RegistraDependencias.Registrar(services);

            services.AddHostedService<WorkerRabbit>();
        }

        private void ConfigEnvoiromentVariables(IServiceCollection services)
        {
            ConfigurarConexoes(services);
            ConfigurarRabbitmq(services);
            ConfigurarRabbitmqLog(services);
            ConfigurarTelemetria(services);
            ConfigurarElasticSearch(services);
            ConfigurarCoresso(services);
            ConfigurarEol(services);
        }

        private void ConfigurarEol(IServiceCollection services)
        {
            var eolOptions = new EolOptions();
            Configuration.GetSection(EolOptions.Secao).Bind(eolOptions, c => c.BindNonPublicProperties = true);
            services.AddSingleton(eolOptions);
        }

        private void ConfigurarCoresso(IServiceCollection services)
        {
            var coressoOptions = new CoressoOptions();
            Configuration.GetSection(CoressoOptions.Secao).Bind(coressoOptions, c => c.BindNonPublicProperties = true);
            services.AddSingleton(coressoOptions);
        }

        private void ConfigurarConexoes(IServiceCollection services)
        {
            var connectionStringOptions = new ConnectionStringOptions();
            Configuration.GetSection(ConnectionStringOptions.Secao).Bind(connectionStringOptions, c => c.BindNonPublicProperties = true);
            services.AddSingleton(connectionStringOptions);
        }

        private void ConfigurarTelemetria(IServiceCollection services)
        {
            var telemetriaOptions = new TelemetriaOptions();
            Configuration.GetSection(TelemetriaOptions.Secao).Bind(telemetriaOptions, c => c.BindNonPublicProperties = true);
            services.AddSingleton(telemetriaOptions);

            var servicoTelemetria = new ServicoTelemetria(telemetriaOptions);
            services.AddSingleton<IServicoTelemetria>(servicoTelemetria);
            DapperExtensionMethods.Init(servicoTelemetria);
        }

        private void ConfigurarRabbitmqLog(IServiceCollection services)
        {
            var rabbitLogOptions = new RabbitLogOptions();
            Configuration.GetSection(RabbitLogOptions.Secao).Bind(rabbitLogOptions, c => c.BindNonPublicProperties = true);
            services.AddSingleton(rabbitLogOptions);

            var factoryLog = new ConnectionFactory
            {
                HostName = rabbitLogOptions.HostName,
                UserName = rabbitLogOptions.UserName,
                Password = rabbitLogOptions.Password,
                VirtualHost = rabbitLogOptions.VirtualHost
            };

            var conexaoRabbitLog = factoryLog.CreateConnection();
            IModel channelLog = conexaoRabbitLog.CreateModel();
        }

        private void ConfigurarElasticSearch(IServiceCollection services)
        {
            var elasticOptions = new ElasticOptions();
            Configuration.GetSection(ElasticOptions.Secao).Bind(elasticOptions, c => c.BindNonPublicProperties = true);
            services.AddSingleton(elasticOptions);

            var nodes = new List<Uri>();
            if (elasticOptions.Urls.Contains(','))
            {
                string[] urls = elasticOptions.Urls.Split(',');
                foreach (string url in urls)
                    nodes.Add(new Uri(url));
            }
            else
            {
                nodes.Add(new Uri(elasticOptions.Urls));
            }

            var connectionPool = new StaticConnectionPool(nodes);
            var connectionSettings = new ConnectionSettings(connectionPool);
            connectionSettings.DefaultIndex(elasticOptions.DefaultIndex);

            if (!string.IsNullOrEmpty(elasticOptions.CertificateFingerprint))
                connectionSettings.CertificateFingerprint(elasticOptions.CertificateFingerprint);

            if (!string.IsNullOrEmpty(elasticOptions.Username) && !string.IsNullOrEmpty(elasticOptions.Password))
                connectionSettings.BasicAuthentication(elasticOptions.Username, elasticOptions.Password);

            var elasticClient = new ElasticClient(connectionSettings);
            services.AddSingleton<IElasticClient>(elasticClient);
        }

        private void ConfigurarRabbitmq(IServiceCollection services)
        {
            var rabbitOptions = new RabbitOptions();
            Configuration.GetSection(RabbitOptions.Secao).Bind(rabbitOptions, c => c.BindNonPublicProperties = true);
            services.AddSingleton(rabbitOptions);

            var factory = new ConnectionFactory
            {
                HostName = rabbitOptions.HostName,
                UserName = rabbitOptions.UserName,
                Password = rabbitOptions.Password,
                VirtualHost = rabbitOptions.VirtualHost
            };

            services.AddSingleton(factory);

            var conexaoRabbit = factory.CreateConnection();
            IModel channel = conexaoRabbit.CreateModel();
            services.AddSingleton(channel);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseElasticApm(Configuration,
              new SqlClientDiagnosticSubscriber(),
              new HttpDiagnosticsSubscriber());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("workerrabbitmq!");
            });
        }
    }
}