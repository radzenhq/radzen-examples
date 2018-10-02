using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ExtendApplicationUser.Models;
using ExtendApplicationUser.Data;

namespace ExtendApplicationUser.Authentication
{
    public partial class ApplicationPrincipalFactory
    {
        partial void OnCreatePrincipal(ClaimsPrincipal principal, ApplicationUser user)
        {
            var identity = principal.Identity as ClaimsIdentity;

            if (!string.IsNullOrEmpty(user.Country))
            {

                // the property will be available at the client-side.
                identity.AddClaim(new Claim("country", user.Country));
            }
        }
    }
}
