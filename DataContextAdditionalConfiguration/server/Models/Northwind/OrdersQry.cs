using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContextAdditionalConfiguration.Models.Northwind
{
  [Table("Orders Qry", Schema = "dbo")]
  public partial class OrdersQry
  {
    [Key]
    public int OrderID
    {
      get;
      set;
    }
    public string CustomerID
    {
      get;
      set;
    }
    public int? EmployeeID
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
    public int? ShipVia
    {
      get;
      set;
    }
    public decimal? Freight
    {
      get;
      set;
    }
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
    public string CompanyName
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
  }
}
