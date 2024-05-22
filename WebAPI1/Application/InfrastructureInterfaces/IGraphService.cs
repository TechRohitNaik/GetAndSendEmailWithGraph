using WebAPI1.Domain.Models;

namespace WebAPI1.Application.InfrastructureInterfaces
{
    public interface IGraphService
    {
        public Task<List<EmailDto>> GetEmailsFromMailbox(string userEmailAcct, int topCount, string filterCondition = null);
        public Task<bool> SendEmailAsync(SendEmailDto email);
    }
}
