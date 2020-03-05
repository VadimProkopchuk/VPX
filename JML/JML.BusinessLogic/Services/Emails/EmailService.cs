using System.Threading.Tasks;
using JML.BusinessLogic.Core.Contracts.Emails;
using JML.BusinessLogic.Core.Contracts.Systems;
using JML.Domain;
using JML.Models;

namespace JML.BusinessLogic.Services.Emails
{
    public class EmailService : IEmailService
    {
        private readonly ISmtpDeliveryService smtpDeliveryService;

        public EmailService(ISmtpDeliveryService smtpDeliveryService)
        {
            this.smtpDeliveryService = smtpDeliveryService;
        }

        public async Task SendRegistrationMailAsync(User user)
        {
            var command = GetSendToCommand("Test message", user.Email, "TEST");

            await smtpDeliveryService.SendAsync(command);
        }


        private SendEmailCommand GetSendToCommand(string message, string recipient, string subject)
        {
            var sendEmailCommand = new SendEmailCommand
            {
                Subject = subject,
                EmailTo = new[] { recipient },
                Body = message,
                IsBodyHtml = false
            };

            return sendEmailCommand;
        }

    }
}
