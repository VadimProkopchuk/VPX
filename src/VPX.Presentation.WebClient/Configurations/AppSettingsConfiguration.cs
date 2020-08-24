using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VPX.Models.Settings;

namespace VPX.Presentation.WebClient.Configurations
{
    public static class AppSettingsConfiguration
    {
        public static AppSettings ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            return appSettingsSection.Get<AppSettings>();
        }
    }
}
