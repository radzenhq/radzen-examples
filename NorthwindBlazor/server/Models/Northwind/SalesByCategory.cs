using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  [Table("Sales by Category", Schema = "dbo")]
  public partial class SalesByCategory
  {
    [Key]
    public int CategoryID
    {
      get;
      set;
    }
    public string CategoryName
    {
      get;
      set;
    }
    public string ProductName
    {
      get;
      set;
    }
    public decimal? ProductSales
    {
      get;
      set;
    }
  }
}
