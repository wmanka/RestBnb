using RestBnb.Core.Constants;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace RestBnb.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SendGridClient _emailClient;

        public EmailSender()
        {
            _emailClient = new SendGridClient(EnvironmentVariables.SendGridApiKey);
        }

        public async Task<Response> SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = MailHelper.CreateSingleEmail(
                new EmailAddress(ApplicationConstants.ApplicationEmail, ApplicationConstants.ApplicationName),
                new EmailAddress(email),
                subject,
                message,
                $"<strong>{message}</strong>");

            return await _emailClient.SendEmailAsync(emailMessage);
        }
    }
}
