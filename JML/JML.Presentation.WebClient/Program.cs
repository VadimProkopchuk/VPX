using JML.DataAccess.Context;
using JML.Presentation.WebClient.Infrastructure.Managers.Migrations;
using JML.Presentation.WebClient.Infrastructure.Managers.Migrations.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JML.Presentation.WebClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<AppDbContext>((context, serviceProvider) =>
                {
                    new DefaultAdminSeed().Seed(context);
                })
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging => logging.AddConsole());
    }
}
