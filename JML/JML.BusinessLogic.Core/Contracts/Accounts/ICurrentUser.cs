using System.Threading.Tasks;
using JML.ApiModels;

namespace JML.BusinessLogic.Core.Contracts.Accounts
{
    public interface ICurrentUser
    {
        Task<UserModel> GetCurrentUserAsync();
    }
}
