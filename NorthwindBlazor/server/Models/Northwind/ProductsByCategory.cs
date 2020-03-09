using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  [Table("Products by Category", Schema = "dbo")]
  public partial class ProductsByCategory
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
    public string QuantityPerUnit
    {
      get;
      set;
    }
    public Int16? UnitsInStock
    {
      get;
      set;
    }
    public bool Discontinued
    {
      get;
      set;
    }
  }
}
