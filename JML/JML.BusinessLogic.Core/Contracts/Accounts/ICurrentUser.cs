using JML.Domain;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Core.Contracts.Accounts
{
    public interface ICurrentUser
    {
        Task<User> GetCurrentUserAync();
    }
}
