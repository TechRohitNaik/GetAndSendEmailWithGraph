using WebAPI1.Application.InfrastructureInterfaces;
using WebAPI1.Domain.Models;

namespace WebAPI1.Application
{
    public class EmailService : IEmailService
    {
        private readonly IGraphService _graphService;
        public EmailService(IGraphService graphService)
        {
            _graphService = graphService;
        }
        public async Task<List<EmailDto>> GetEmails(string mailboxEmail, int topCount, string filterCondition)
        {
            try
            {
                var emails = await _graphService.GetEmailsFromMailbox(mailboxEmail, topCount, filterCondition);
                return emails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> SendEmailAsync(SendEmailDto email)
        {
            try
            {
                var status = await _graphService.SendEmailAsync(email);
                return status;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
