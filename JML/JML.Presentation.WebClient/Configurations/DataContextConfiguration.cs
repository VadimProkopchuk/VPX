using JML.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JML.Presentation.WebClient.Configurations
{
    public static class DataContextConfiguration
    {
        public static void ConfigureDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"])
                .UseLazyLoadingProxies());
        }
    }
}
