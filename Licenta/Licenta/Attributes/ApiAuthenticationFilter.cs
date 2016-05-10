using Licenta.Services.Implementation.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;

namespace Licenta.Attributes
{
    public class ApiAuthenticationFilter : GenericAuthenticationFilter
    {
        public ApiAuthenticationFilter()
        {
        }

        public ApiAuthenticationFilter(bool isActive)
            : base(isActive)
        {
        }

        protected override bool OnAuthorizeUser(string username, string token, HttpActionContext actionContext)
        {
            var provider = new AuthorizationServiceProvider();
            if (provider != null)
            {
                var user = provider.Authenticate(username, token);
                if (user != null)
                {
                    var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                    if (basicAuthenticationIdentity != null)
                        basicAuthenticationIdentity.UserId = user.Id;

                    return true;
                }
            }

            return false;
        }
    }
}