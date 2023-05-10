using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRMBlazorWasmRBS.Server.Filters
{
    public class ApplicationAuthorizeFilter : AuthorizeFilter
    {
        public ApplicationAuthorizeFilter(AuthorizationPolicy policy): base(policy)
        {
        }

        public override Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Path.StartsWithSegments("/Account") || context.HttpContext.Request.Path.StartsWithSegments("/Login"))
            {
                return Task.CompletedTask;
            }

            return base.OnAuthorizationAsync(context);
        }
    }
}