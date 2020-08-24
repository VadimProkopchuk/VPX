using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VPX.DataAccess.Context;

namespace VPX.Presentation.WebClient.Configurations
{
    public static class DataContextConfiguration
    {
        public static void ConfigureDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"],
                        sqlConfig => sqlConfig.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                    .UseLazyLoadingProxies());
        }
    }
}