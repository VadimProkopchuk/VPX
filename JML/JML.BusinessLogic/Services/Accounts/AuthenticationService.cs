using System;
using System.Threading.Tasks;
using JML.BusinessLogic.Core.Contracts.Accounts;
using JML.BusinessLogic.Core.Contracts.Systems;
using JML.BusinessLogic.Core.Contracts.Users;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using JML.Models;

namespace JML.BusinessLogic.Services.Accounts
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUsersService usersService;
        private readonly IPasswordEncrypter passwordEncrypter;
        private readonly IDataContext dataContext;
        private readonly IJwtService jwtService;

        public AuthenticationService(IUsersService usersService,
            IPasswordEncrypter passwordEncrypter,
            IDataContext dataContext,
            IJwtService jwtService)
        {
            this.usersService = usersService;
            this.passwordEncrypter = passwordEncrypter;
            this.dataContext = dataContext;
            this.jwtService = jwtService;
        }

        public async Task<JwtModel> AuthAsync(string email, string password)
        {
            var user = await usersService.GetByEmailAsync(email);

            if (user == null)
            {
                throw new ArgumentOutOfRangeException(nameof(email), "Can't find user");
            }

            if (user.IsLocked)
            {
                throw new ApplicationException("User is locked.");
            }

            if (user.Password == passwordEncrypter.Encrypt(password))
            {
                await UpdateUserAttempts(user, attempts: 0);
                return jwtService.GetToken(user);
            } 
            else
            {
                await UpdateUserAttempts(user, user.CountOfInvalidAttempts + 1);
                throw new ApplicationException("Invalid user password");
            }
        }

        private async Task UpdateUserAttempts(User user, int attempts)
        {
            user.CountOfInvalidAttempts = attempts;
            user.IsLocked = user.CountOfInvalidAttempts > 4;

            await dataContext.SaveChangesAsync();
        }
    }
}
