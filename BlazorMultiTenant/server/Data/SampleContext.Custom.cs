using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BlazorMultiTenant.Data
{
    public partial class SampleContext
    {
        private readonly HttpContext context;
        private readonly Multitenancy multitenancy;

        public SampleContext(DbContextOptions<SampleContext> options, ApplicationIdentityDbContext identityDbContext, IHttpContextAccessor httpContextAccessor, Multitenancy mt) : base(options)
        {
            context = httpContextAccessor.HttpContext;
            multitenancy = mt;
        }

        public SampleContext(IHttpContextAccessor httpContextAccessor, Multitenancy mt, ApplicationIdentityDbContext identityDbContext)
        {
            context = httpContextAccessor.HttpContext;
            multitenancy = mt;
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
