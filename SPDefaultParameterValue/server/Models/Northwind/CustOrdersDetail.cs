using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpDefaultParameterValue.Models.Northwind
{
  [Table("CustOrdersDetail", Schema = "dbo")]
  public partial class CustOrdersDetail
  {
    [Key]
    public string ProductName
    {
      get;
      set;
    }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public decimal? UnitPrice
    {
      get;
      set;
    }
    public Int16 Quantity
    {
      get;
      set;
    }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int? Discount
    {
      get;
      set;
    }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public decimal? ExtendedPrice
    {
      get;
      set;
    }
  }
}
