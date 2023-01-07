using MimeKit;

namespace EmailService
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(null, x)));

            Subject = subject;
            Content = content;
        }

        public Message(string to, string subject, string content)
        {
            To = new List<MailboxAddress>
            {
                new MailboxAddress(null, to)
            };

            Subject = subject;
            Content = content;
        }
    }
}
