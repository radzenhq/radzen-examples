using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtendAdditionalProperty.Models.Northwind
{
  [Table("Products Above Average Price", Schema = "dbo")]
  public partial class ProductsAboveAveragePrice
  {
    [Key]
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
