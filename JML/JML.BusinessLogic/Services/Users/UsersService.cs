using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Accounts;
using JML.BusinessLogic.Core.Contracts.Systems;
using JML.BusinessLogic.Core.Contracts.Users;
using JML.BusinessLogic.Mappings.Users;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IAppEntityRepository<User> usersRepository;
        private readonly IDataContext dataContext;
        private readonly ICurrentUser currentUser;
        private readonly IPasswordEncrypter passwordEncrypter;

        public UsersService(IAppEntityRepository<User> usersRepository,
            IDataContext dataContext,
            ICurrentUser currentUser,
            IPasswordEncrypter passwordEncrypter)
        {
            this.usersRepository = usersRepository;
            this.dataContext = dataContext;
            this.currentUser = currentUser;
            this.passwordEncrypter = passwordEncrypter;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await usersRepository.GetQuery().FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<UserProfileModel> GetProfile(Guid id)
        {
            var user = await usersRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);
            return UserProfileMap.Map(user);
        }

        public async Task<List<UserModel>> GetTeachers()
        {
            var teachers = await usersRepository
                .GetQuery()
                .Where(x => x.UserRoles.Any(x => x.Role == Domain.Enums.Role.Teacher))
                .Distinct()
                .ToListAsync();

            return teachers.Select(UserMap.Map).ToList();
        }

        public async Task<List<UserWithoutGroupModel>> GetUsersWithoutGroups()
        {
            var users = await usersRepository.GetQuery().Where(x => x.GroupId == null).ToListAsync();

            return users
                .Select(x => new UserWithoutGroupModel
                {
                    Value = x.Id,
                    Display = $"{x.FirstName} {x.LastName}"
                })
                .ToList();
        }

        public async Task<bool> HasUserByEmailAsync(string email)
        {
            return await usersRepository.GetQuery().AnyAsync(x => x.Email == email);
        }

        public async Task<UserProfileModel> Lock(Guid userId)
        {
            var user = await usersRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                user.IsLocked = true;
                await dataContext.SaveChangesAsync();
            }

            return UserProfileMap.Map(user);
        }

        public async Task<UserProfileModel> Unlock(Guid userId)
        {
            var user = await usersRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                user.IsLocked = false;
                user.CountOfInvalidAttempts = 0;
                await dataContext.SaveChangesAsync();
            }

            return UserProfileMap.Map(user);
        }

        public async Task<UserModel> Update(UserUpdatesModel model)
        {
            var user = await currentUser.GetUser();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            if (!String.IsNullOrEmpty(model.NewPassword))
            {
                var encryptedPassword = passwordEncrypter.Encrypt(model.Password);
                if (user.Password == encryptedPassword)
                {
                    user.Password = passwordEncrypter.Encrypt(model.NewPassword);
                }
            }

            if (model.Image != null)
            {
                user.AvatarBase64 = model.Image;
            }

            await dataContext.SaveChangesAsync();

            return await currentUser.GetCurrentUserAsync();
        }
    }
}
