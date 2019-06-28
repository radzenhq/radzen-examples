using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  [Table("CustOrderHist", Schema = "dbo")]
  public partial class CustOrderHist
  {
    [Key]
    public string ProductName
    {
      get;
      set;
    }
    public int? Total
    {
      get;
      set;
    }
  }
}
