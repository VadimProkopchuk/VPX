﻿using System;
using System.Collections.Generic;
using System.Linq;
using VPX.BusinessLogic.Core.Contracts.Systems;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VPX.DataAccess.Context;
using VPX.Domain;
using VPX.Enums;
using VPX.Models.Settings;
using VPX.Models.Settings.Seed;

namespace VPX.Presentation.WebClient.Infrastructure.Managers.Migrations.Seed
{
    public class DefaultAdminSeed
    {
        public void Seed(AppDbContext context, IServiceProvider serviceProvider, ILogger<AppDbContext> logger)
        {
            logger.LogInformation("Default Admin Seed started...");

            var users = context.Set<User>();
            var options = serviceProvider.GetService(typeof(IOptions<AppSettings>)) as IOptions<AppSettings>;
            var seedSettings = options.Value.Seed;
            var passwordEncrypter = serviceProvider.GetService(typeof(IPasswordEncrypter)) as IPasswordEncrypter;

            if (!users.Any())
            {
                users.AddRange(GetAdmins(seedSettings, passwordEncrypter));
                context.SaveChanges();
            }

            logger.LogInformation("Default Admin Seed completed.");
        }

        private IEnumerable<User> GetAdmins(SeedSettings settings, IPasswordEncrypter passwordEncrypter)
        {
            var password = passwordEncrypter.Encrypt(settings.DefaultAdmin.Password);

            yield return new User()
            {
                Email = settings.DefaultAdmin.Email,
                FirstName = settings.DefaultAdmin.FirstName,
                LastName = settings.DefaultAdmin.LastName,
                Password = password,
                UserRoles = new List<UserRole> { new UserRole { Role = Role.Admin } }
            };
        }
    }
}
