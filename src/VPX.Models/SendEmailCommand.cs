using System.Collections.Generic;
using System.Net.Mail;

namespace VPX.Models
{
    public class SendEmailCommand
    {
        public string[] EmailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromEmailTitle { get; set; }
        public IReadOnlyList<Attachment> AttachmentFiles { get; set; }
        public bool IsBodyHtml { get; set; } = true;
        public string BccAddress { get; set; }
    }
}
