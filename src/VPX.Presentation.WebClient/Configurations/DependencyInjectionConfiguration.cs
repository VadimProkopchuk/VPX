using VPX.BusinessLogic.Core.Contracts.Accounts;
using VPX.BusinessLogic.Core.Contracts.Emails;
using VPX.BusinessLogic.Core.Contracts.Groups;
using VPX.BusinessLogic.Core.Contracts.KnowledgeTests;
using VPX.BusinessLogic.Core.Contracts.Lectures;
using VPX.BusinessLogic.Core.Contracts.Systems;
using VPX.BusinessLogic.Core.Contracts.Tags;
using VPX.BusinessLogic.Core.Contracts.TestTemplates;
using VPX.BusinessLogic.Core.Contracts.Users;
using Microsoft.Extensions.DependencyInjection;
using VPX.BusinessLogic.Services.Accounts;
using VPX.BusinessLogic.Services.Emails;
using VPX.BusinessLogic.Services.Groups;
using VPX.BusinessLogic.Services.KnowledgeTests;
using VPX.BusinessLogic.Services.Lectures;
using VPX.BusinessLogic.Services.Systems;
using VPX.BusinessLogic.Services.Tags;
using VPX.BusinessLogic.Services.TestTemplates;
using VPX.BusinessLogic.Services.Users;
using VPX.DataAccess.Context;
using VPX.DataAccess.Core.Contracts;
using VPX.DataAccess.Repository;
using VPX.Presentation.WebClient.Infrastructure.Context;

namespace VPX.Presentation.WebClient.Configurations
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
            services.AddScoped<IContextService, HttpContextService>();
            services.AddScoped<ICurrentUser, CurrentUserService>();
            services.AddScoped<ILectureService, LectureService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ILectureTagBinder, LectureTagBinder>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISmtpDeliveryService, SmtpDeliveryService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IBase64TextConverter, Base64TextConverter>();
            services.AddScoped<IPasswordGenerator, PasswordGenerator>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<ILiteratureService, LiteratureService>();
            services.AddScoped<IUserRolesService, UserRolesService>();
            services.AddScoped<ITestTemplatesService, TestTemplatesService>();
            services.AddScoped<IKnowledgeTestResultService, KnowledgeTestResultService>();
            services.AddScoped<IKnowledgeTestService, KnowledgeTestService>();
        }
    }
}
