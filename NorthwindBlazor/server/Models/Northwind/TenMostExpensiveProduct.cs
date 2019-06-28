using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  [Table("Ten Most Expensive Products", Schema = "dbo")]
  public partial class TenMostExpensiveProduct
  {
    [Key]
    public string TenMostExpensiveProducts
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
