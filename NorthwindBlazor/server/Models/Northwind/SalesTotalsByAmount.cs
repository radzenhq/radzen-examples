using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  [Table("Sales Totals by Amount", Schema = "dbo")]
  public partial class SalesTotalsByAmount
  {
    public decimal? SaleAmount
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
    public string CompanyName
    {
      get;
      set;
    }
    public DateTime? ShippedDate
    {
      get;
      set;
    }
  }
}
