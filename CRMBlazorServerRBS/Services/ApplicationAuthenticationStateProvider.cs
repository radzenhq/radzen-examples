using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

using CRMBlazorServerRBS.Models;

namespace CRMBlazorServerRBS
{
    public class ApplicationAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly SecurityService securityService;
        private ApplicationAuthenticationState authenticationState;

        public ApplicationAuthenticationStateProvider(SecurityService securityService)
        {
            this.securityService = securityService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            try
            {
                var state = await GetApplicationAuthenticationStateAsync();

                if (state.IsAuthenticated)
                {
                    identity = new ClaimsIdentity(state.Claims.Select(c => new Claim(c.Type, c.Value)), "CRMBlazorServerRBS");
                }
            }
            catch (HttpRequestException ex)
            {
            }

            var result = new AuthenticationState(new ClaimsPrincipal(identity));

            await securityService.InitializeAsync(result);

            return result;
        }

        private async Task<ApplicationAuthenticationState> GetApplicationAuthenticationStateAsync()
        {
            if (authenticationState == null)
            {
                authenticationState = await securityService.GetAuthenticationStateAsync();
            }

            return authenticationState;
        }
    }
}