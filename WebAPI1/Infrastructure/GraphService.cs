using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users.Item.SendMail;
using WebAPI1.Application.InfrastructureInterfaces;
using WebAPI1.Domain.Models;

namespace WebAPI1.Infrastructure
{
    public class GraphService : IGraphService
    {
        private readonly IOptions<GraphConfig> _config;
        public GraphService(IOptions<GraphConfig> config)
        {
            _config = config;
        }
        public async Task<List<EmailDto>> GetEmailsFromMailbox(string userEmailAcct, int topCount, string filterCondition = null)
        {
            var emails = new List<EmailDto>();
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var graphServiceClient = new GraphServiceClient(new ClientSecretCredential(_config.Value.TenantId, _config.Value.ClientId, _config.Value.ClientSecret), scopes);
            List<Message> messages = new List<Message>();
            try
            {
                var inboxMessages = await graphServiceClient.Users[userEmailAcct].MailFolders["inbox"].Messages
                                        .GetAsync((requestConfiguration) =>
                                        {
                                            requestConfiguration.QueryParameters.Top = topCount;
                                            if (!string.IsNullOrEmpty(filterCondition))
                                                requestConfiguration.QueryParameters.Filter = filterCondition;
                                            requestConfiguration.Headers.Add("Prefer", "outlook.body-content-type=\"text\"");
                                        });

                if (inboxMessages == null)
                {
                    return emails;
                }
                else
                {
                    foreach (var message in inboxMessages.Value)
                    {
                        var emailMessage = new EmailDto();
                        emailMessage.IsRead = message.IsRead.Value;
                        emailMessage.Subject = message.Subject;
                        emailMessage.Id = message.Id;
                        emailMessage.Body = message.Body.Content;
                        emails.Add(emailMessage);
                    }
                }

                return emails;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> SendEmailAsync(SendEmailDto email)
        {
            try
            {
                var emails = new List<EmailDto>();
                var scopes = new[] { "https://graph.microsoft.com/.default" };
                var graphServiceClient = new GraphServiceClient(new ClientSecretCredential(_config.Value.TenantId, _config.Value.ClientId, _config.Value.ClientSecret), scopes);

                var toRecipient = new List<Recipient>();
                foreach (var recipient in email.ToEmail)
                {
                    toRecipient.Add(new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = recipient
                        }
                    });
                }
                var body = new SendMailPostRequestBody
                {
                    Message = new Message
                    {
                        Subject = email.Subject,
                        Body = new ItemBody
                        {
                            ContentType = BodyType.Text,
                            Content = email.Body
                        },
                        ToRecipients = toRecipient
                    }
                };
                await graphServiceClient.Users[email.FromEmail].SendMail.PostAsync(body);
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }
    }
}
