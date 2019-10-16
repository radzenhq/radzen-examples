using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using NgSelectDataGridCheckBox.Models.Sample;

namespace NgSelectDataGridCheckBox.Data
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

            builder.Entity<NgSelectDataGridCheckBox.Models.Sample.OrderDetail>()
                  .HasOne(i => i.Order)
                  .WithMany(i => i.OrderDetails)
                  .HasForeignKey(i => i.OrderId)
                  .HasPrincipalKey(i => i.Id);
            builder.Entity<NgSelectDataGridCheckBox.Models.Sample.OrderDetail>()
                  .HasOne(i => i.Product)
                  .WithMany(i => i.OrderDetails)
                  .HasForeignKey(i => i.ProductId)
                  .HasPrincipalKey(i => i.Id);


            builder.Entity<NgSelectDataGridCheckBox.Models.Sample.Order>()
                  .Property(p => p.OrderDate)
                  .HasColumnType("date");

            this.OnModelBuilding(builder);
        }


        public DbSet<NgSelectDataGridCheckBox.Models.Sample.Order> Orders
        {
          get;
          set;
        }

        public DbSet<NgSelectDataGridCheckBox.Models.Sample.OrderDetail> OrderDetails
        {
          get;
          set;
        }

        public DbSet<NgSelectDataGridCheckBox.Models.Sample.Product> Products
        {
          get;
          set;
        }
    }
}
