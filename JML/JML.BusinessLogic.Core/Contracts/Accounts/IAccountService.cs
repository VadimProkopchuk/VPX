using System.Threading.Tasks;
using JML.ApiModels;
using JML.Models;

namespace JML.BusinessLogic.Core.Contracts.Accounts
{
    public interface IAccountService
    {
        Task<JwtModel> AuthAsync(string email, string password);
        Task VerifyAsync(VerificationUserModel user);
        Task<UserModel> RegisterAsync(RegisterUserModel user);
    }
}
