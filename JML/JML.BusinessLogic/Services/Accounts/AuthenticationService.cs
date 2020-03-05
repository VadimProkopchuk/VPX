using System;
using System.Threading.Tasks;
using JML.BusinessLogic.Core.Contracts.Accounts;
using JML.BusinessLogic.Core.Contracts.Systems;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using JML.Models;

namespace JML.BusinessLogic.Services.Accounts
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordEncrypter passwordEncrypter;
        private readonly IDataContext dataContext;
        private readonly IJwtService jwtService;

        public AuthenticationService(IPasswordEncrypter passwordEncrypter,
            IDataContext dataContext,
            IJwtService jwtService)
        {
            this.passwordEncrypter = passwordEncrypter;
            this.dataContext = dataContext;
            this.jwtService = jwtService;
        }

        public async Task<JwtModel> AuthAsync(User user, string password)
        {
            if (user.Password == passwordEncrypter.Encrypt(password))
            {
                await UpdateUserAttempts(user, attempts: 0);
                return jwtService.GetToken(user);
            }

            await UpdateUserAttempts(user, user.CountOfInvalidAttempts + 1);
            throw new ApplicationException("Неверный пароль.");
        }

        private async Task UpdateUserAttempts(User user, int attempts)
        {
            user.CountOfInvalidAttempts = attempts;
            user.IsLocked = user.CountOfInvalidAttempts > 4;

            await dataContext.SaveChangesAsync();
        }
    }
}
