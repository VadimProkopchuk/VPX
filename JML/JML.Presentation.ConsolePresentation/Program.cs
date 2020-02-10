using JML.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore.SqlServer;
using JML.Domain;

namespace JML.Presentation.ConsolePresentation
{
    class Program
    {
        private static IConfigurationRoot configurationRoot;

        static IConfigurationRoot GetConfiguration()
        {
            if (configurationRoot == null)
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
             
                configurationRoot = builder.Build();
            }

            return configurationRoot;
        }

        static void Main(string[] args)
        {
            var config = GetConfiguration();
            var connectionOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(config.GetConnectionString("JML.Database"))
                .Options;
//            using var appDbContext = new AppDbContext(connectionOptions);

            //var usersSet = appDbContext.Set<User>();

            




            Console.WriteLine("Hello World!");
        }


    }
}
