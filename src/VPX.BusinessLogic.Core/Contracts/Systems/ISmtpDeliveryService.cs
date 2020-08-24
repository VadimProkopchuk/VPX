using System.Threading.Tasks;
using VPX.Models;

namespace VPX.BusinessLogic.Core.Contracts.Systems
{
    public interface ISmtpDeliveryService
    {
        Task<bool> SendAsync(SendEmailCommand sendEmailCommand);
    }
}
