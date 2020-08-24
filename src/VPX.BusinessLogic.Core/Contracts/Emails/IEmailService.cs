using System.Threading.Tasks;
using VPX.ApiModels;

namespace VPX.BusinessLogic.Core.Contracts.Emails
{
    public interface IEmailService
    {
        Task SendVerificationMailAsync(VerificationUserModel user, string verificationCode);
        Task SendRestoreAccessMailAsync(UserModel user, string password);
    }
}
