using System.Threading.Tasks;
using VPX.ApiModels;
using VPX.Domain;

namespace VPX.BusinessLogic.Core.Contracts.Accounts
{
    public interface ICurrentUser
    {
        Task<UserModel> GetCurrentUserAsync();
        Task<User> GetUser();
    }
}
