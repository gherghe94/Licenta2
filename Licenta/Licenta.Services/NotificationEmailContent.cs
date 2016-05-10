using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services
{
    public class NotificationEmailContent : IEmailContent
    {
        public string Content { get; set; }

        public string Subject { get; set; }

        public NotificationEmailContent(string content, string subject)
        {
            Content = content;
            Subject = subject;  
        }

        public string GetSubject()
        {
            return Subject ?? "[Orar] Notification - Unit Bv";
        }

        public string GetBody()
        {
            return Content ?? string.Empty;
        }
    }
}
