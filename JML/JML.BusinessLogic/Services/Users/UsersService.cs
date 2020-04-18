using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Users;
using JML.BusinessLogic.Mappings.Users;
using JML.DataAccess.Core.Contracts;
using JML.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<UserModel>> GetTeachers()
        {
            var teachers = await usersRepository
                .GetQuery()
                .Where(x => x.UserRoles.Any(x => x.Role == Domain.Enums.Role.Teacher))
                .ToListAsync();

            return teachers.Select(UserMap.Map).ToList();
        }

        public async Task<bool> HasUserByEmailAsync(string email)
        {
            return await usersRepository.GetQuery().AnyAsync(x => x.Email == email);
        }
    }
}
