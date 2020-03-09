using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  [Table("Order Details", Schema = "dbo")]
  public partial class OrderDetail
  {
    [Key]
    public int OrderID
    {
      get;
      set;
    }
    public Order Order { get; set; }
    [Key]
    public int ProductID
    {
      get;
      set;
    }
    public Product Product { get; set; }
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
  }
}
