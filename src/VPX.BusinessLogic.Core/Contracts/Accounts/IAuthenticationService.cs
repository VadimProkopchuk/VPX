using System.Threading.Tasks;
using VPX.Domain;
using VPX.Models;

namespace VPX.BusinessLogic.Core.Contracts.Accounts
{
    public interface IAuthenticationService
    {
        Task<JwtModel> AuthAsync(User user, string password);
    }
}
