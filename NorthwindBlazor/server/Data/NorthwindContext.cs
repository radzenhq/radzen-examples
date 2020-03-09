using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using NorthwindBlazor.Models.Northwind;

namespace NorthwindBlazor.Data
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

        builder.Entity<NorthwindBlazor.Models.Northwind.AlphabeticalListOfProduct>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.CategorySalesFor1997>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.CurrentProductList>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.CustOrderHist>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.CustOrdersDetail>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.CustOrdersOrder>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.CustomerAndSuppliersByCity>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.EmployeeSalesByCountry>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.Invoice>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.OrderDetailsExtended>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.OrderSubtotal>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.OrdersQry>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.ProductSalesFor1997>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.ProductsAboveAveragePrice>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.ProductsByCategory>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.QuarterlyOrder>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.SalesByCategory>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.SalesByCategory1>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.SalesByYear>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.SalesTotalsByAmount>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.SummaryOfSalesByQuarter>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.SummaryOfSalesByYear>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.TenMostExpensiveProduct>().HasNoKey();
        builder.Entity<NorthwindBlazor.Models.Northwind.CustomerCustomerDemo>().HasKey(table => new {
          table.CustomerID, table.CustomerTypeID
        });
        builder.Entity<NorthwindBlazor.Models.Northwind.EmployeeTerritory>().HasKey(table => new {
          table.EmployeeID, table.TerritoryID
        });
        builder.Entity<NorthwindBlazor.Models.Northwind.OrderDetail>().HasKey(table => new {
          table.OrderID, table.ProductID
        });
        builder.Entity<NorthwindBlazor.Models.Northwind.RolePermission>().HasKey(table => new {
          table.RoleName, table.PermissionId
        });
        builder.Entity<NorthwindBlazor.Models.Northwind.CustomerCustomerDemo>()
              .HasOne(i => i.Customer)
              .WithMany(i => i.CustomerCustomerDemos)
              .HasForeignKey(i => i.CustomerID)
              .HasPrincipalKey(i => i.CustomerID);
        builder.Entity<NorthwindBlazor.Models.Northwind.CustomerCustomerDemo>()
              .HasOne(i => i.CustomerDemographic)
              .WithMany(i => i.CustomerCustomerDemos)
              .HasForeignKey(i => i.CustomerTypeID)
              .HasPrincipalKey(i => i.CustomerTypeID);
        builder.Entity<NorthwindBlazor.Models.Northwind.Employee>()
              .HasOne(i => i.Employee1)
              .WithMany(i => i.Employees1)
              .HasForeignKey(i => i.ReportsTo)
              .HasPrincipalKey(i => i.EmployeeID);
        builder.Entity<NorthwindBlazor.Models.Northwind.EmployeeTerritory>()
              .HasOne(i => i.Employee)
              .WithMany(i => i.EmployeeTerritories)
              .HasForeignKey(i => i.EmployeeID)
              .HasPrincipalKey(i => i.EmployeeID);
        builder.Entity<NorthwindBlazor.Models.Northwind.EmployeeTerritory>()
              .HasOne(i => i.Territory)
              .WithMany(i => i.EmployeeTerritories)
              .HasForeignKey(i => i.TerritoryID)
              .HasPrincipalKey(i => i.TerritoryID);
        builder.Entity<NorthwindBlazor.Models.Northwind.Order>()
              .HasOne(i => i.Customer)
              .WithMany(i => i.Orders)
              .HasForeignKey(i => i.CustomerID)
              .HasPrincipalKey(i => i.CustomerID);
        builder.Entity<NorthwindBlazor.Models.Northwind.Order>()
              .HasOne(i => i.Employee)
              .WithMany(i => i.Orders)
              .HasForeignKey(i => i.EmployeeID)
              .HasPrincipalKey(i => i.EmployeeID);
        builder.Entity<NorthwindBlazor.Models.Northwind.Order>()
              .HasOne(i => i.Shipper)
              .WithMany(i => i.Orders)
              .HasForeignKey(i => i.ShipVia)
              .HasPrincipalKey(i => i.ShipperID);
        builder.Entity<NorthwindBlazor.Models.Northwind.OrderDetail>()
              .HasOne(i => i.Order)
              .WithMany(i => i.OrderDetails)
              .HasForeignKey(i => i.OrderID)
              .HasPrincipalKey(i => i.OrderID);
        builder.Entity<NorthwindBlazor.Models.Northwind.OrderDetail>()
              .HasOne(i => i.Product)
              .WithMany(i => i.OrderDetails)
              .HasForeignKey(i => i.ProductID)
              .HasPrincipalKey(i => i.ProductID);
        builder.Entity<NorthwindBlazor.Models.Northwind.Product>()
              .HasOne(i => i.Supplier)
              .WithMany(i => i.Products)
              .HasForeignKey(i => i.SupplierID)
              .HasPrincipalKey(i => i.SupplierID);
        builder.Entity<NorthwindBlazor.Models.Northwind.Product>()
              .HasOne(i => i.Category)
              .WithMany(i => i.Products)
              .HasForeignKey(i => i.CategoryID)
              .HasPrincipalKey(i => i.CategoryID);
        builder.Entity<NorthwindBlazor.Models.Northwind.Territory>()
              .HasOne(i => i.Region)
              .WithMany(i => i.Territories)
              .HasForeignKey(i => i.RegionID)
              .HasPrincipalKey(i => i.RegionID);

        builder.Entity<NorthwindBlazor.Models.Northwind.Order>()
              .Property(p => p.Freight)
              .HasDefaultValueSql("((0))");

        builder.Entity<NorthwindBlazor.Models.Northwind.OrderDetail>()
              .Property(p => p.UnitPrice)
              .HasDefaultValueSql("((0))");

        builder.Entity<NorthwindBlazor.Models.Northwind.OrderDetail>()
              .Property(p => p.Quantity)
              .HasDefaultValueSql("((1))");

        builder.Entity<NorthwindBlazor.Models.Northwind.OrderDetail>()
              .Property(p => p.Discount)
              .HasDefaultValueSql("((0))");

        builder.Entity<NorthwindBlazor.Models.Northwind.Product>()
              .Property(p => p.UnitPrice)
              .HasDefaultValueSql("((0))");

        builder.Entity<NorthwindBlazor.Models.Northwind.Product>()
              .Property(p => p.UnitsInStock)
              .HasDefaultValueSql("((0))");

        builder.Entity<NorthwindBlazor.Models.Northwind.Product>()
              .Property(p => p.UnitsOnOrder)
              .HasDefaultValueSql("((0))");

        builder.Entity<NorthwindBlazor.Models.Northwind.Product>()
              .Property(p => p.ReorderLevel)
              .HasDefaultValueSql("((0))");

        builder.Entity<NorthwindBlazor.Models.Northwind.Product>()
              .Property(p => p.Discontinued)
              .HasDefaultValueSql("((0))");


        builder.Entity<NorthwindBlazor.Models.Northwind.CustOrdersOrder>()
              .Property(p => p.OrderDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.CustOrdersOrder>()
              .Property(p => p.RequiredDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.CustOrdersOrder>()
              .Property(p => p.ShippedDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.Employee>()
              .Property(p => p.BirthDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.Employee>()
              .Property(p => p.HireDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.EmployeeSalesByCountry>()
              .Property(p => p.ShippedDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.Invoice>()
              .Property(p => p.OrderDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.Invoice>()
              .Property(p => p.RequiredDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.Invoice>()
              .Property(p => p.ShippedDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.Order>()
              .Property(p => p.OrderDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.Order>()
              .Property(p => p.RequiredDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.Order>()
              .Property(p => p.ShippedDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.OrdersQry>()
              .Property(p => p.OrderDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.OrdersQry>()
              .Property(p => p.RequiredDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.OrdersQry>()
              .Property(p => p.ShippedDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.SalesByYear>()
              .Property(p => p.ShippedDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.SalesTotalsByAmount>()
              .Property(p => p.ShippedDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.SummaryOfSalesByQuarter>()
              .Property(p => p.ShippedDate)
              .HasColumnType("datetime");

        builder.Entity<NorthwindBlazor.Models.Northwind.SummaryOfSalesByYear>()
              .Property(p => p.ShippedDate)
              .HasColumnType("datetime");

        this.OnModelBuilding(builder);
    }


    public DbSet<NorthwindBlazor.Models.Northwind.AlphabeticalListOfProduct> AlphabeticalListOfProducts
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.Category> Categories
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.CategorySalesFor1997> CategorySalesFor1997s
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.CurrentProductList> CurrentProductLists
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.CustOrderHist> CustOrderHists
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.CustOrdersDetail> CustOrdersDetails
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.CustOrdersOrder> CustOrdersOrders
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.Customer> Customers
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.CustomerAndSuppliersByCity> CustomerAndSuppliersByCities
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.CustomerCustomerDemo> CustomerCustomerDemos
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.CustomerDemographic> CustomerDemographics
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.Employee> Employees
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.EmployeeSalesByCountry> EmployeeSalesByCountries
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.EmployeeTerritory> EmployeeTerritories
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.Invoice> Invoices
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.Order> Orders
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.OrderDetail> OrderDetails
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.OrderDetailsExtended> OrderDetailsExtendeds
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.OrderSubtotal> OrderSubtotals
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.OrdersQry> OrdersQries
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.Product> Products
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.ProductSalesFor1997> ProductSalesFor1997s
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.ProductsAboveAveragePrice> ProductsAboveAveragePrices
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.ProductsByCategory> ProductsByCategories
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.QuarterlyOrder> QuarterlyOrders
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.Region> Regions
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.RolePermission> RolePermissions
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.SalesByCategory> SalesByCategories
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.SalesByCategory1> SalesByCategory1s
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.SalesByYear> SalesByYears
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.SalesTotalsByAmount> SalesTotalsByAmounts
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.Shipper> Shippers
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.SummaryOfSalesByQuarter> SummaryOfSalesByQuarters
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.SummaryOfSalesByYear> SummaryOfSalesByYears
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.Supplier> Suppliers
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.TenMostExpensiveProduct> TenMostExpensiveProducts
    {
      get;
      set;
    }

    public DbSet<NorthwindBlazor.Models.Northwind.Territory> Territories
    {
      get;
      set;
    }
  }
}
