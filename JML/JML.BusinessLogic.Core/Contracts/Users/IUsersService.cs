using JML.Domain;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Core.Contracts.Users
{
    public interface IUsersService
    {
        Task<User> GetByEmailAsync(string email);
        Task<bool> HasUserByEmailAsync(string email);
    }
}
