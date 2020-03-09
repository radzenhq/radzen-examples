using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  [Table("Order Subtotals", Schema = "dbo")]
  public partial class OrderSubtotal
  {
    public int OrderID
    {
      get;
      set;
    }
    public decimal? Subtotal
    {
      get;
      set;
    }
  }
}
