using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  [Table("Products Above Average Price", Schema = "dbo")]
  public partial class ProductsAboveAveragePrice
  {
    public string ProductName
    {
      get;
      set;
    }
    public decimal? UnitPrice
    {
      get;
      set;
    }
  }
}
