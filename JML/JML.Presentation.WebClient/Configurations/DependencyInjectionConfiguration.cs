﻿using JML.BusinessLogic.Core.Contracts.Accounts;
using JML.BusinessLogic.Core.Contracts.Systems;
using JML.BusinessLogic.Core.Contracts.Users;
using JML.BusinessLogic.Services.Account;
using JML.BusinessLogic.Services.Systems;
using JML.BusinessLogic.Services.Users;
using JML.DataAccess.Context;
using JML.DataAccess.Core.Contracts;
using JML.DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace JML.Presentation.WebClient.Configurations
{
    internal static class DependencyInjectionConfiguration
    {
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IDataContext, DataContext>();
            services.AddScoped(typeof(IAppEntityRepository<>), typeof(AppEntityRepository<>));

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IPasswordEncrypter, PasswordEncrypter>();
            services.AddScoped<ISystemTimeService, SystemTimeService>();
            services.AddScoped<IUsersService, UsersService>();


        }
    }
}