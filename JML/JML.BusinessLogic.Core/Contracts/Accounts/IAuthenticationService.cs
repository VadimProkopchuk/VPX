using JML.Models;
using System.Threading.Tasks;
using JML.Domain;

namespace JML.BusinessLogic.Core.Contracts.Accounts
{
    public interface IAuthenticationService
    {
        Task<JwtModel> AuthAsync(User user, string password);
    }
}
