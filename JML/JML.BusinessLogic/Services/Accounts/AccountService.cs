using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Accounts;
using JML.BusinessLogic.Core.Contracts.Emails;
using JML.BusinessLogic.Core.Contracts.Systems;
using JML.BusinessLogic.Core.Contracts.Users;
using JML.BusinessLogic.Mappings.Users;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using JML.Domain.Enums;
using JML.Models;

namespace JML.BusinessLogic.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IUsersService usersService;
        private readonly IAuthenticationService authenticationService;
        private readonly IPasswordEncrypter passwordEncrypter;
        private readonly IAppEntityRepository<User> usersRepository;
        private readonly IDataContext dataContext;
        private readonly IEmailService emailService;

        public AccountService(IUsersService usersService, 
            IAuthenticationService authenticationService,
            IPasswordEncrypter passwordEncrypter,
            IAppEntityRepository<User> usersRepository,
            IDataContext dataContext,
            IEmailService emailService)
        {
            this.usersService = usersService;
            this.authenticationService = authenticationService;
            this.passwordEncrypter = passwordEncrypter;
            this.usersRepository = usersRepository;
            this.dataContext = dataContext;
            this.emailService = emailService;
        }

        public async Task<JwtModel> AuthAsync(string email, string password)
        {
            var user = await usersService.GetByEmailAsync(email);

            if (user == null)
            {
                throw new ApplicationException("Пользователь не найден.");
            }

            if (user.IsLocked)
            {
                throw new ApplicationException("Пользователь заблокирован.");
            }

            return await authenticationService.AuthAsync(user, password);
        }

        public async Task<UserModel> RegisterAsync(RegisterUserModel registerUser)
        {
            var user = await usersService.GetByEmailAsync(registerUser.Email);

            if (user != null)
            {
                throw new ApplicationException("Пользователь с таким email уже существует.");
            }

            var encryptedPassword = passwordEncrypter.Encrypt(registerUser.Password);
            user = new User
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Email = registerUser.Email,
                Password = encryptedPassword,
                UserRoles = new List<UserRole> { new UserRole { Role = Role.Student } }
            };

            usersRepository.Add(user);
            
            await dataContext.SaveChangesAsync();
            await emailService.SendRegistrationMailAsync(user);

            return UserMap.Map(user);
        }
    }
}
