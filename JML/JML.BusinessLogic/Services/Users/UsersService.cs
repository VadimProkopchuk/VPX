using JML.BusinessLogic.Core.Contracts.Users;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IAppEntityRepository<User> usersRepository;

        public UsersService(IAppEntityRepository<User> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await usersRepository.GetQuery().FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> HasUserByEmailAsync(string email)
        {
            return await usersRepository.GetQuery().AnyAsync(x => x.Email == email);
        }
    }
}
