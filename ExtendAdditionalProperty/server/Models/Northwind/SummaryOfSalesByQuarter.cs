using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtendAdditionalProperty.Models.Northwind
{
  [Table("Summary of Sales by Quarter", Schema = "dbo")]
  public partial class SummaryOfSalesByQuarter
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
