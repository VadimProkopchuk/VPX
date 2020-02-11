using JML.Models;
using System.Threading.Tasks;

namespace JML.BusinessLogic.Core.Contracts.Accounts
{
    public interface IAuthenticationService
    {
        Task<JwtModel> AuthAsync(string email, string password);
    }
}
