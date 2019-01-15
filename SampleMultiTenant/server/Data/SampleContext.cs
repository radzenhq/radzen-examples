using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using MultiTenantSample.Models.Sample;

namespace MultiTenantSample.Data
{
  public partial class SampleContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public SampleContext(DbContextOptions<SampleContext> options):base(options)
    {
    }

    public SampleContext()
    {
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
