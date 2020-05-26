using System.Threading.Tasks;
using JML.ApiModels;
using JML.Domain;

namespace JML.BusinessLogic.Core.Contracts.Accounts
{
    public interface ICurrentUser
    {
        Task<UserModel> GetCurrentUserAsync();
        Task<User> GetUser();
    }
}
