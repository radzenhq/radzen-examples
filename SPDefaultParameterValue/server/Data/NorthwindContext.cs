using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using SpDefaultParameterValue.Models.Northwind;

namespace SpDefaultParameterValue.Data
{
  public partial class NorthwindContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public NorthwindContext(DbContextOptions<NorthwindContext> options):base(options)
    {
    }

    public NorthwindContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<SpDefaultParameterValue.Models.Northwind.EmployeeSalesByCountry>().HasKey(table => new {
          table.LastName, table.OrderID
        });

        this.OnModelBuilding(builder);
    }


    public DbSet<SpDefaultParameterValue.Models.Northwind.CustOrderHist> CustOrderHists
    {
      get;
      set;
    }

    public DbSet<SpDefaultParameterValue.Models.Northwind.CustOrdersDetail> CustOrdersDetails
    {
      get;
      set;
    }

    public DbSet<SpDefaultParameterValue.Models.Northwind.CustOrdersOrder> CustOrdersOrders
    {
      get;
      set;
    }

    public DbSet<SpDefaultParameterValue.Models.Northwind.EmployeeSalesByCountry> EmployeeSalesByCountries
    {
      get;
      set;
    }

    public DbSet<SpDefaultParameterValue.Models.Northwind.SalesByCategory1> SalesByCategory1s
    {
      get;
      set;
    }

    public DbSet<SpDefaultParameterValue.Models.Northwind.SalesByYear> SalesByYears
    {
      get;
      set;
    }

    public DbSet<SpDefaultParameterValue.Models.Northwind.TenMostExpensiveProduct> TenMostExpensiveProducts
    {
      get;
      set;
    }
  }
}
