using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  [Table("Order Details Extended", Schema = "dbo")]
  public partial class OrderDetailsExtended
  {
    public int OrderID
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
  }
}
