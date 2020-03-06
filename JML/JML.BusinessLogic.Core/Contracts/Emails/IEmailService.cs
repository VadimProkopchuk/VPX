using System.Threading.Tasks;
using JML.ApiModels;

namespace JML.BusinessLogic.Core.Contracts.Emails
{
    public interface IEmailService
    {
        Task SendVerificationMailAsync(VerificationUserModel user, string verificationCode);
    }
}
