using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using Sample.Models.Sample;

namespace Sample.Data
{
    public partial class SampleContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IHttpContextAccessor httpAccessor;

        public SampleContext(IHttpContextAccessor httpAccessor, DbContextOptions<SampleContext> options):base(options)
        {
            this.httpAccessor = httpAccessor;
        }

        public SampleContext(IHttpContextAccessor httpAccessor)
        {
            this.httpAccessor = httpAccessor;
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Sample.Models.Sample.OrderDetail>()
                  .HasOne(i => i.Order)
                  .WithMany(i => i.OrderDetails)
                  .HasForeignKey(i => i.OrderId)
                  .HasPrincipalKey(i => i.Id);
            builder.Entity<Sample.Models.Sample.OrderDetail>()
                  .HasOne(i => i.Product)
                  .WithMany(i => i.OrderDetails)
                  .HasForeignKey(i => i.ProductId)
                  .HasPrincipalKey(i => i.Id);

            this.OnModelBuilding(builder);
        }


        public DbSet<Sample.Models.Sample.Order> Orders
        {
          get;
          set;
        }

        public DbSet<Sample.Models.Sample.OrderDetail> OrderDetails
        {
          get;
          set;
        }

        public DbSet<Sample.Models.Sample.Product> Products
        {
          get;
          set;
        }
    }
}
