namespace WebAPI1.Domain.Models
{
    public class SendEmailDto
    {
        public string FromEmail { get; set; }
        public List<string> ToEmail { get; set; }
        public List<string> CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
