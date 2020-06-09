using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazorCrmWasm.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;

namespace BlazorCrmWasm.Data
{
    public class ApplicationIdentityDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationIdentityDbContext(
            DbContextOptions<ApplicationIdentityDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
    }
}
