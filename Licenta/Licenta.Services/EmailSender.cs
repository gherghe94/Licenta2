using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services
{
    public class EmailSender
    {
        private const string username = "andrei.gherghelau@gmail.com";
        private const string password = "Gherghelau123";
        private const string gmailHost = "smtp.gmail.com";

        private MailAddress To { get; set; }

        private MailAddress From { get; set; }

        private MailMessage Mail { get; set; }

        private SmtpClient Smtp { get; set; }

        private IEmailContent Content { get; set; }

        public EmailSender(string to, IEmailContent content)
        {
            Content = content;
            Configure(to);
        }

        public void Send()
        {
            Mail = GetMail();
            SendMail();
        }

        private void SendMail()
        {
            try
            {
                Smtp.Send(Mail);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private MailMessage GetMail()
        {
            var mail = new MailMessage(From, To);
            mail.Subject = Content.GetSubject();
            mail.Body = Content.GetBody();
            mail.IsBodyHtml = true;
            return mail;
        }

        private void Configure(string to)
        {
            Smtp = new SmtpClient();
            Smtp.Host = gmailHost;
            Smtp.Port = 587;
            Smtp.Credentials = new NetworkCredential(username, password);
            Smtp.EnableSsl = true;

            To = new MailAddress(to);
            From = new MailAddress(username, "Orar Unit Bv");
        }
    }
}
