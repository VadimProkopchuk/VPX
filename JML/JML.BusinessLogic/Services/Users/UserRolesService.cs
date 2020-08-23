using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Users;
using JML.BusinessLogic.Mappings.Users;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using JML.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Services.Users
{
    public class UserRolesService : IUserRolesService
    {
        private readonly IAppEntityRepository<User> usersRepository;
        private readonly IAppEntityRepository<UserRole> userRolesRepository;
        private readonly IDataContext dataContext;

        public UserRolesService(IAppEntityRepository<User> usersRepository,
            IAppEntityRepository<UserRole> userRolesRepository,
            IDataContext dataContext)
        {
            this.usersRepository = usersRepository;
            this.userRolesRepository = userRolesRepository;
            this.dataContext = dataContext;
        }

        public async Task<UserProfileModel> AddRole(Guid userId, Role role)
        {
            var user = await usersRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null && !user.UserRoles.Any(x => x.Role == role))
            {
                user.UserRoles.Add(new UserRole { Role = role});
                await dataContext.SaveChangesAsync();
            }
            
            return UserProfileMap.Map(user);
        }

        public async Task<UserProfileModel> RemoveRole(Guid userId, Role role)
        {
            var user = await usersRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                var userRoles = user.UserRoles.Where(x => x.Role == role).ToList();

                foreach (var userRole in userRoles)
                {
                    userRolesRepository.Remove(userRole);
                }

                await dataContext.SaveChangesAsync();
            }

            return UserProfileMap.Map(user);
        }
    }
}
