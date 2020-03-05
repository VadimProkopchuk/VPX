using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JML.Presentation.WebClient.Infrastructure.Managers.Migrations
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using var scope = host.Services.CreateScope();
            
            var services = scope.ServiceProvider;
            using var context = services.GetRequiredService<TContext>();
            
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var contextName = typeof(TContext).Name;

            try
            {
                logger.LogInformation($"Migrating database associated with context {contextName}.");

                context.Database.Migrate();
                seeder(context, host.Services);

                logger.LogInformation($"Database associated with context {contextName} has been migrated.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occured while migrating the database used on context {contextName}.");
                Console.WriteLine(ex);
            }

            return host;
        }
    }
}
