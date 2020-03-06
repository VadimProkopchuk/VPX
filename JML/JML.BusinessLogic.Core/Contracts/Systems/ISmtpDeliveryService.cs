using System.Threading.Tasks;
using JML.Models;

namespace JML.BusinessLogic.Core.Contracts.Systems
{
    public interface ISmtpDeliveryService
    {
        Task<bool> SendAsync(SendEmailCommand sendEmailCommand);
    }
}
