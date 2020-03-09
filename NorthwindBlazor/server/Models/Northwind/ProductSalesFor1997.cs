using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  [Table("Product Sales for 1997", Schema = "dbo")]
  public partial class ProductSalesFor1997
  {
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
