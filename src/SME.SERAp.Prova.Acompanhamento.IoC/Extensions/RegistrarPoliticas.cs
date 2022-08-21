﻿using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Registry;
using SME.SERAp.Prova.Acompanhamento.Infra.Policies;
using System;

namespace SME.SERAp.Prova.Acompanhamento.IoC.Extensions
{
    public static class RegistrarPoliticas
    {
        public static void AddPoliticas(this IServiceCollection services)
        {
            IPolicyRegistry<string> registry = services.AddPolicyRegistry();

            Random jitterer = new();
            var policyFila = Policy.Handle<Exception>()
              .WaitAndRetryAsync(3,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                      + TimeSpan.FromMilliseconds(jitterer.Next(0, 30)));

            registry.Add(PoliticaPolly.PublicaFila, policyFila);
        }
    }
}
