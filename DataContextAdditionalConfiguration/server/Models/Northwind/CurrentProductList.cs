using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContextAdditionalConfiguration.Models.Northwind
{
  [Table("Current Product List", Schema = "dbo")]
  public partial class CurrentProductList
  {
    [Key]
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
  }
}
