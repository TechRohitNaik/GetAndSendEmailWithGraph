namespace WebAPI1.Domain.Models
{
    public class EmailDto
    {
        public string Id { get; set; }
        public bool IsRead { get; set; }
        public string FromEmail { get; set; }
        public List<string> ToEmail { get; set; }
        public List<string> CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
