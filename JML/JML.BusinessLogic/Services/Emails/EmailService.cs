using System.Text;
using System.Threading.Tasks;
using JML.ApiModels;
using JML.BusinessLogic.Core.Contracts.Emails;
using JML.BusinessLogic.Core.Contracts.Systems;
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

        public async Task SendVerificationMailAsync(VerificationUserModel user, string verificationCode)
        {
            var messageBuilder = new StringBuilder($"Здравствуйте, ${user.FirstName} {user.LastName}!");

            messageBuilder.AppendLine();
            messageBuilder.AppendLine("Для продолжение регстрации введите код на сайте.");
            messageBuilder.AppendLine($"Код: {verificationCode}");

            var command = GetSendToCommand(messageBuilder.ToString(), user.Email, "Подтверждение регистрации");

            await smtpDeliveryService.SendAsync(command);
        }

        public async Task SendRestoreAccessMailAsync(UserModel user, string password)
        {
            var messageBuilder = new StringBuilder($"Здравствуйте, {user.FirstName} {user.LastName}!");

            messageBuilder.AppendLine();
            messageBuilder.AppendLine("Для восстановления доступа был установлен новый пароль.");
            messageBuilder.AppendLine($"Пароль: {password}");

            var command = GetSendToCommand(messageBuilder.ToString(), user.Email, "Восстановление доступа");

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
