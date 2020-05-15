using JML.BusinessLogic.Core.Contracts.Accounts;
using JML.BusinessLogic.Core.Contracts.Systems;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using JML.ApiModels;
using JML.BusinessLogic.Mappings.Users;
using System;

namespace JML.BusinessLogic.Services.Accounts
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

            return UserMap.Map(currentUser);
        }
    }
}
