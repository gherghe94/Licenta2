using Licenta.Services.Implementation;
using Licenta.Services.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Licenta.Filters
{
    public class IdentityBasicAuthenticationAttribute : BasicAuthenticationAttribute
    {
        private bool CanAccess(string username, string password)
        {
            List<IAccessGranter> accessGranterServices = new List<IAccessGranter>();

            accessGranterServices.Add(new TeacherService());
            accessGranterServices.Add(new StudentService());
            accessGranterServices.Add(new AdminService());

            foreach (var item in accessGranterServices)
            {
                if (item.HasAccess(username, password))
                    return true;
            }

            return false;
        }

        protected override async Task<IPrincipal> AuthenticateAsync(string userName, string password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // Custom stuff to autenticate 

            if (!CanAccess(userName, password))
            {
                return null;
            }

            // Create a ClaimsIdentity with all the claims for this user.
            Claim nameClaim = new Claim(ClaimTypes.Name, userName);
            List<Claim> claims = new List<Claim> { nameClaim };

            // important to set the identity this way, otherwise IsAuthenticated will be false
            // see: http://leastprivilege.com/2012/09/24/claimsidentity-isauthenticated-and-authenticationtype-in-net-4-5/             
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Basic");

            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
    }
}