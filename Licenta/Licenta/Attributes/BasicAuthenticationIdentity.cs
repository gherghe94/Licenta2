using Licenta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Licenta.Attributes
{
    public class BasicAuthenticationIdentity : GenericIdentity
    {
        public string Token { get; set; }
        
        public string UserName { get; set; }
       
        public int UserId { get; set; }

        public UserType Type { get; set; }

        public BasicAuthenticationIdentity(string userName, string token)
            : base(userName, "Bearer")
        {
            Token = token;
            UserName = userName;
        }
    }
}
