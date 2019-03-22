using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace AuditTrail
{
    public partial class Startup
    {
        partial void OnConfigureServices(IServiceCollection services)
        {
            var policy = new AuthorizationPolicyBuilder()
            {
              AuthenticationSchemes = new [] {"Bearer"}
            }
            .RequireAuthenticatedUser()
            .Build();

            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter(policy));
            });
        }
    }
}
