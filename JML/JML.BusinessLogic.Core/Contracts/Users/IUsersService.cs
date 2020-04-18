using JML.ApiModels;
using JML.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Core.Contracts.Users
{
    public interface IUsersService
    {
        Task<User> GetByEmailAsync(string email);
        Task<bool> HasUserByEmailAsync(string email);
        Task<List<UserModel>> GetTeachers();
    }
}
