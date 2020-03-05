using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JML.BusinessLogic.Core.Contracts.Systems;
using JML.Models;
using JML.Models.Settings;
using Microsoft.Extensions.Options;

namespace JML.BusinessLogic.Services.Systems
{
    public class SmtpDeliveryService : ISmtpDeliveryService
    {
        private readonly EmailSettings emailSettings;

        public SmtpDeliveryService(IOptions<AppSettings> appSettings)
        {
            emailSettings = appSettings.Value.Email;
        }

        public async Task<bool> SendAsync(SendEmailCommand sendEmailCommand)
        {
            if (sendEmailCommand == null)
            {
                throw new ArgumentNullException(nameof(sendEmailCommand));
            }
            if (!sendEmailCommand.EmailTo.Any())
            {
                throw new ArgumentNullException($"'{nameof(sendEmailCommand.EmailTo)}' is not provided.");
            }

            using var message = new MailMessage();
            foreach (var emailTo in sendEmailCommand.EmailTo.Distinct())
            {
                if (!IsValidEmail(emailTo))
                {
                    throw new ArgumentException($"Email to address '{emailTo}' is invalid.");
                }

                message.To.Add(emailTo);
            }

            message.Subject = sendEmailCommand.Subject;
            message.IsBodyHtml = sendEmailCommand.IsBodyHtml;
            message.Body = sendEmailCommand.Body;
            message.From = string.IsNullOrWhiteSpace(sendEmailCommand.FromEmailTitle)
                ? new MailAddress(emailSettings.FromTitle)
                : new MailAddress(emailSettings.FromTitle, sendEmailCommand.FromEmailTitle);

            if (sendEmailCommand.AttachmentFiles?.Any() == true)
            {
                foreach (var attachment in sendEmailCommand.AttachmentFiles)
                {
                    message.Attachments.Add(attachment);
                }
            }

            if (!string.IsNullOrEmpty(sendEmailCommand.BccAddress))
            {
                message.Bcc.Add(sendEmailCommand.BccAddress);
            }

            using var smtpClient = new SmtpClient(emailSettings.Host, emailSettings.Port)
            {
                UseDefaultCredentials = emailSettings.UseDefaultCredentials
            };

            if (!smtpClient.UseDefaultCredentials)
            {
                smtpClient.Credentials = new NetworkCredential(emailSettings.UserName, emailSettings.Password);
            }

            smtpClient.EnableSsl = emailSettings.EnableSsl;

            var isEmailSent = false;
            var attempt = 0;

            while (!isEmailSent && attempt < emailSettings.MaxSendAttempts)
            {
                try
                {
                    attempt++;
                    await smtpClient.SendMailAsync(message);
                    isEmailSent = true;
                }
                catch (SmtpFailedRecipientsException ex)
                {
                    var statusCode = ex.StatusCode;
                    var statuses = new[]
                    {
                        SmtpStatusCode.MailboxBusy, SmtpStatusCode.MailboxUnavailable,
                        SmtpStatusCode.TransactionFailed
                    };

                    if (statuses.Contains(statusCode))
                    {
                        if (attempt >= emailSettings.MaxSendAttempts)
                        {
                            if (!emailSettings.IsExceptionSuppressionEnabled)
                            {
                                throw;
                            }
                        }

                        await Task.Delay(emailSettings.AttemptWaitMs);
                    }
                }
                catch (Exception)
                {
                    if (!emailSettings.IsExceptionSuppressionEnabled)
                    {
                        throw;
                    }
                }
            }

            return isEmailSent;
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                var rfc2822EmailFormatRegex =
                    new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                        RegexOptions.IgnoreCase);
                return rfc2822EmailFormatRegex.IsMatch(email);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
