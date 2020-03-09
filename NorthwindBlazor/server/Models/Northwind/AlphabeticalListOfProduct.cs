using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  [Table("Alphabetical list of products", Schema = "dbo")]
  public partial class AlphabeticalListOfProduct
  {
    public int ProductID
    {
      get;
      set;
    }
    public string ProductName
    {
      get;
      set;
    }
    public int? SupplierID
    {
      get;
      set;
    }
    public int? CategoryID
    {
      get;
      set;
    }
    public string QuantityPerUnit
    {
      get;
      set;
    }
    public decimal? UnitPrice
    {
      get;
      set;
    }
    public Int16? UnitsInStock
    {
      get;
      set;
    }
    public Int16? UnitsOnOrder
    {
      get;
      set;
    }
    public Int16? ReorderLevel
    {
      get;
      set;
    }
    public bool Discontinued
    {
      get;
      set;
    }
    public string CategoryName
    {
      get;
      set;
    }
  }
}
