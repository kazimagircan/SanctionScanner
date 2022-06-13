using SanctionScanner.Infrastructure.Email.Model;
using System.Threading.Tasks;

namespace SanctionScanner.Infrastructure.Email.Service
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}
