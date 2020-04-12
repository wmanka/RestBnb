using SendGrid;
using System.Threading.Tasks;

namespace RestBnb.Infrastructure.Services
{
    public interface IEmailSender
    {
        Task<Response> SendEmailAsync(string email, string subject, string message);
    }
}
