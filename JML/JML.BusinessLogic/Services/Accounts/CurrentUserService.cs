using JML.BusinessLogic.Core.Contracts.Accounts;
using JML.BusinessLogic.Core.Contracts.Systems;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Services.Accounts
{
    public class CurrentUserService : ICurrentUser
    {
        private readonly IContextService contextService;
        private readonly IAppEntityRepository<User> usersRepository;

        private User currentUser;

        public CurrentUserService(IContextService contextService,
            IAppEntityRepository<User> usersRepository)
        {
            this.contextService = contextService;
            this.usersRepository = usersRepository;
        }

        public async Task<User> GetCurrentUserAync()
        {
            if (currentUser != null)
            {
                return currentUser;
            }

            var userId = contextService.GetCurrentUserId();

            if (userId != null)
            {
                currentUser = await usersRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == userId.Value);
            }

            return currentUser;
        }
    }
}
