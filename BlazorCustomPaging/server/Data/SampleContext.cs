using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using BlazorCustomPaging.Models.Sample;

namespace BlazorCustomPaging.Data
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

        builder.Entity<BlazorCustomPaging.Models.Sample.OrderDetail>()
              .HasOne(i => i.Order)
              .WithMany(i => i.OrderDetails)
              .HasForeignKey(i => i.OrderId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<BlazorCustomPaging.Models.Sample.OrderDetail>()
              .HasOne(i => i.Product)
              .WithMany(i => i.OrderDetails)
              .HasForeignKey(i => i.ProductId)
              .HasPrincipalKey(i => i.Id);


        builder.Entity<BlazorCustomPaging.Models.Sample.Order>()
              .Property(p => p.OrderDate)
              .HasColumnType("date");

        this.OnModelBuilding(builder);
    }


    public DbSet<BlazorCustomPaging.Models.Sample.Order> Orders
    {
      get;
      set;
    }

    public DbSet<BlazorCustomPaging.Models.Sample.OrderDetail> OrderDetails
    {
      get;
      set;
    }

    public DbSet<BlazorCustomPaging.Models.Sample.Product> Products
    {
      get;
      set;
    }
  }
}
