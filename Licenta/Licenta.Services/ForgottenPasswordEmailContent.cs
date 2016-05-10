using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Services
{
    public class ForgottenPasswordEmailContent : IEmailContent
    {
        public string GetSubject()
        {
            return "[Orar] Forgotten password - Universitatea Transilvania Bv";
        }

        public string GetBody()
        {
            return "";
        }
    }
}
