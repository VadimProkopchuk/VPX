using JML.DataAccess.Context;
using JML.Presentation.WebClient.Infrastructure.Managers.Migrations;
using JML.Presentation.WebClient.Infrastructure.Managers.Migrations.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace JML.Presentation.WebClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase(new List<Action<AppDbContext, IServiceProvider, ILogger<AppDbContext>>>() {
                    (context, serviceProvider, logger) => new DefaultAdminSeed().Seed(context, serviceProvider, logger),
                })
                .Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging => logging.AddConsole());
    }
}
