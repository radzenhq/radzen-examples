using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using MultiTenantSample.Models.Sample;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MultiTenantSample.Data
{
    public partial class SampleContext
    {
        private readonly HttpContext context;
        private readonly Multitenancy multitenancy;

        public SampleContext(DbContextOptions<SampleContext> options, ApplicationIdentityDbContext identityDbContext, IHttpContextAccessor httpContextAccessor, Multitenancy mt) : base(options)
        {
            context = httpContextAccessor.HttpContext;
            multitenancy = mt;
            Database.EnsureCreated();
            identityDbContext.Database.Migrate();
        }

        public SampleContext(IHttpContextAccessor httpContextAccessor, Multitenancy mt, ApplicationIdentityDbContext identityDbContext)
        {
            context = httpContextAccessor.HttpContext;
            multitenancy = mt;
            Database.EnsureCreated();
            identityDbContext.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (multitenancy != null && context != null)
            {
                var tenant = multitenancy.Tenants
                        .Where(t => t.Hostnames.Contains(context.Request.Host.Value)).FirstOrDefault();

                if (tenant != null)
                {
                    optionsBuilder.UseSqlServer(tenant.ConnectionString);
                }
            }
        }
    }
}
