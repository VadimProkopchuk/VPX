using System.Threading.Tasks;
using JML.ApiModels;
using JML.Domain;

namespace JML.BusinessLogic.Core.Contracts.Emails
{
    public interface IEmailService
    {
        Task SendVerificationMailAsync(VerificationUserModel user, string verificationCode);
        Task SendRestoreAccessMailAsync(UserModel user, string password);
    }
}
