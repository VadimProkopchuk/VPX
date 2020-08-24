using System;
using System.Threading.Tasks;
using VPX.BusinessLogic.Core.Contracts.Accounts;
using VPX.BusinessLogic.Core.Contracts.Systems;
using Microsoft.EntityFrameworkCore;
using VPX.ApiModels;
using VPX.BusinessLogic.Mappings.Users;
using VPX.DataAccess.Core.Contracts;
using VPX.Domain;

namespace VPX.BusinessLogic.Services.Accounts
{
    public class CurrentUserService : ICurrentUser
    {
        private readonly IContextService contextService;
        private readonly IDataContext dataContext;
        private readonly IAppEntityRepository<User> usersRepository;
        private User currentUser;

        public CurrentUserService(IContextService contextService,
            IDataContext dataContext,
            IAppEntityRepository<User> usersRepository)
        {
            this.contextService = contextService;
            this.dataContext = dataContext;
            this.usersRepository = usersRepository;
        }

        public async Task<UserModel> GetCurrentUserAsync()
        {
            await LoadUser();
            return UserMap.Map(currentUser);
        }

        public async Task<User> GetUser()
        {
            await LoadUser();
            return currentUser;
        }

        private async Task LoadUser()
        {
            if (currentUser == null)
            {
                var userId = contextService.GetCurrentUserId();
                if (userId != null)
                {
                    currentUser = await usersRepository
                        .GetQuery()
                        .Include(x => x.UserRoles)
                        .Include(x => x.Group)
                        .FirstOrDefaultAsync(x => x.Id == userId.Value);
                    currentUser.ActiveAt = DateTime.Now;

                    await dataContext.SaveChangesAsync();
                }
            }
        }
    }
}
