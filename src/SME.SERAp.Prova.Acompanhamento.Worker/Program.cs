using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace SME.SERAp.Prova.Acompanhamento.Worker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(a => a.AddUserSecrets(Assembly.GetExecutingAssembly()))
           .UseStartup<Startup>();
    }
}
