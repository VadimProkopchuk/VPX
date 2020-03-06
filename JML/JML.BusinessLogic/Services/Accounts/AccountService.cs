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
        private readonly IBase64TextConverter base64TextConverter;
        private readonly IPasswordGenerator passwordGenerator;

        public AccountService(IUsersService usersService, 
            IAuthenticationService authenticationService,
            IPasswordEncrypter passwordEncrypter,
            IAppEntityRepository<User> usersRepository,
            IDataContext dataContext,
            IEmailService emailService,
            IBase64TextConverter base64TextConverter,
            IPasswordGenerator passwordGenerator)
        {
            this.usersService = usersService;
            this.authenticationService = authenticationService;
            this.passwordEncrypter = passwordEncrypter;
            this.usersRepository = usersRepository;
            this.dataContext = dataContext;
            this.emailService = emailService;
            this.base64TextConverter = base64TextConverter;
            this.passwordGenerator = passwordGenerator;
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

            var verificationCode = GetVerificationCode(registerUser.Email);
            if (registerUser.VerificationCode?.Trim() != verificationCode.Trim())
            {
                throw new ApplicationException("Неверный код подтверждения.");
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

            return UserMap.Map(user);
        }

        public async Task RestoreAccess(RestoreAccessModel model)
        {
            var user = await usersService.GetByEmailAsync(model.Email);

            if (user == null)
            {
                throw new ApplicationException("Пользователь не найден.");
            }

            var password = passwordGenerator.Generate(8);
            var encryptedPassword = passwordEncrypter.Encrypt(password);

            user.Password = encryptedPassword;
            user.IsLocked = false;
            user.CountOfInvalidAttempts = 0;

            await dataContext.SaveChangesAsync();
            await emailService.SendRestoreAccessMailAsync(UserMap.Map(user), password);
        }

        public async Task VerifyAsync(VerificationUserModel user)
        {
            var verificationCode = GetVerificationCode(user.Email);
            await emailService.SendVerificationMailAsync(user, verificationCode);
        }

        private string GetVerificationCode(string email)
        {
            return base64TextConverter.ToBase64(email);
        }
    }
}
