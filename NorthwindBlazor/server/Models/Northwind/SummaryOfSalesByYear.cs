using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  [Table("Summary of Sales by Year", Schema = "dbo")]
  public partial class SummaryOfSalesByYear
  {
    public DateTime? ShippedDate
    {
      get;
      set;
    }
    [Key]
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
