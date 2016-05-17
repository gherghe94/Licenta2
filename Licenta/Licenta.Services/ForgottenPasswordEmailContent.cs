using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services
{
    public class ForgottenPasswordEmailContent : IEmailContent
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ForgottenPasswordEmailContent(string fullName, string user, string pwd)
        {
            FullName = fullName;
            Username = user;
            Password = pwd;
        }

        public string GetSubject()
        {
            return "[Orar] Forgotten password - Universitatea Transilvania Bv";
        }

        public string GetBody()
        {
            return string.Format(
                "<b> Hello {0}, <br> Your Credentials : <br> User: {1} <br> Password: {2} <br> <i> For more information contact orar.unitbv.ro </i> </b>",
                FullName, Username, Password);
        }
    }
}
