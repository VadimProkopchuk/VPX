using System.Threading.Tasks;
using JML.Domain;

namespace JML.BusinessLogic.Core.Contracts.Emails
{
    public interface IEmailService
    {
        Task SendRegistrationMailAsync(User user);
    }
}
