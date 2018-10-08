using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContextAdditionalConfiguration.Models.Northwind
{
  [Table("Invoices", Schema = "dbo")]
  public partial class Invoice
  {
    public string ShipName
    {
      get;
      set;
    }
    public string ShipAddress
    {
      get;
      set;
    }
    public string ShipCity
    {
      get;
      set;
    }
    public string ShipRegion
    {
      get;
      set;
    }
    public string ShipPostalCode
    {
      get;
      set;
    }
    public string ShipCountry
    {
      get;
      set;
    }
    public string CustomerID
    {
      get;
      set;
    }
    [Key]
    public string CustomerName
    {
      get;
      set;
    }
    public string Address
    {
      get;
      set;
    }
    public string City
    {
      get;
      set;
    }
    public string Region
    {
      get;
      set;
    }
    public string PostalCode
    {
      get;
      set;
    }
    public string Country
    {
      get;
      set;
    }
    public string Salesperson
    {
      get;
      set;
    }
    public int OrderID
    {
      get;
      set;
    }
    public DateTime? OrderDate
    {
      get;
      set;
    }
    public DateTime? RequiredDate
    {
      get;
      set;
    }
    public DateTime? ShippedDate
    {
      get;
      set;
    }
    public string ShipperName
    {
      get;
      set;
    }
    public int ProductID
    {
      get;
      set;
    }
    public string ProductName
    {
      get;
      set;
    }
    public decimal UnitPrice
    {
      get;
      set;
    }
    public Int16 Quantity
    {
      get;
      set;
    }
    public float Discount
    {
      get;
      set;
    }
    public decimal? ExtendedPrice
    {
      get;
      set;
    }
    public decimal? Freight
    {
      get;
      set;
    }
  }
}
