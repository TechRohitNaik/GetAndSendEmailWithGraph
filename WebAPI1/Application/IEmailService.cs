using WebAPI1.Domain.Models;

namespace WebAPI1.Application
{
    public interface IEmailService
    {
        public Task<List<EmailDto>> GetEmails(string mailboxEmail, int topCount, string filterCondition);
        public Task<bool> SendEmailAsync(SendEmailDto email);
    }
}
