using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContextAdditionalConfiguration.Models.Northwind
{
  [Table("Category Sales for 1997", Schema = "dbo")]
  public partial class CategorySalesFor1997
  {
    [Key]
    public string CategoryName
    {
      get;
      set;
    }
    public decimal? CategorySales
    {
      get;
      set;
    }
  }
}
