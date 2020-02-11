using JML.BusinessLogic.Core.Contracts.Accounts;
using JML.BusinessLogic.Core.Contracts.Systems;
using JML.BusinessLogic.Core.Contracts.Users;
using JML.DataAccess.Core.Contracts;
using JML.Models;
using System;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Services.Account
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

            JwtModel jwt = null;
            if (!user.IsLocked && user.Password == passwordEncrypter.Encrypt(password))
            {
                jwt = jwtService.GetToken(user);
            } 
            else
            {
                user.CountOfInvalidAttempts++;
                user.IsLocked = user.CountOfInvalidAttempts > 4;
            }

            await dataContext.SaveChangesAsync();

            if (user.IsLocked)
            {
                throw new ApplicationException("User is locked.");
            }

            return jwt;
        }
    }
}
