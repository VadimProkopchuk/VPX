using System.Threading.Tasks;
using VPX.ApiModels;
using VPX.Models;

namespace VPX.BusinessLogic.Core.Contracts.Accounts
{
    public interface IAccountService
    {
        Task<JwtModel> AuthAsync(string email, string password);
        Task VerifyAsync(VerificationUserModel user);
        Task<UserModel> RegisterAsync(RegisterUserModel user);
        Task RestoreAccess(RestoreAccessModel model);
    }
}
