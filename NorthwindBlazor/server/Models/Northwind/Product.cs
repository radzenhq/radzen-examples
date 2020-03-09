using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  [Table("Products", Schema = "dbo")]
  public partial class Product
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductID
    {
      get;
      set;
    }


    public ICollection<OrderDetail> OrderDetails { get; set; }
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
    public Supplier Supplier { get; set; }
    public int? CategoryID
    {
      get;
      set;
    }
    public Category Category { get; set; }
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
  }
}
