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
    public partial class SampleContext : Microsoft.EntityFrameworkCore.DbContext
    {

        private readonly HttpContext context;
        private readonly Multitenancy multitenancy;

        public SampleContext(IHttpContextAccessor httpContextAccessor, Multitenancy mt)
        {
            context = httpContextAccessor.HttpContext;
            multitenancy = mt;
            Database.EnsureCreated();
        }

        public SampleContext(DbContextOptions<SampleContext> options, IHttpContextAccessor httpContextAccessor, Multitenancy mt) : base(options)
        {
            context = httpContextAccessor.HttpContext;
            multitenancy = mt;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var tenant = multitenancy.Tenants
                    .Where(t => t.Hostnames.Contains(context.Request.Host.Value)).FirstOrDefault();

            if (tenant != null)
            {
                optionsBuilder.UseSqlServer(tenant.ConnectionString);
            }
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MultiTenantSample.Models.Sample.OrderDetail>()
                  .HasOne(i => i.Order)
                  .WithMany(i => i.OrderDetails)
                  .HasForeignKey(i => i.OrderId)
                  .HasPrincipalKey(i => i.Id);
            builder.Entity<MultiTenantSample.Models.Sample.OrderDetail>()
                  .HasOne(i => i.Product)
                  .WithMany(i => i.OrderDetails)
                  .HasForeignKey(i => i.ProductId)
                  .HasPrincipalKey(i => i.Id);

            this.OnModelBuilding(builder);
        }


        public DbSet<MultiTenantSample.Models.Sample.Order> Orders
        {
            get;
            set;
        }

        public DbSet<MultiTenantSample.Models.Sample.OrderDetail> OrderDetails
        {
            get;
            set;
        }

        public DbSet<MultiTenantSample.Models.Sample.Product> Products
        {
            get;
            set;
        }
    }
}
