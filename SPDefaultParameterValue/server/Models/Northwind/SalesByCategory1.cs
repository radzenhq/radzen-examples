using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpDefaultParameterValue.Models.Northwind
{
  [Table("SalesByCategory", Schema = "dbo")]
  public partial class SalesByCategory1
  {
    [Key]
    public string ProductName
    {
      get;
      set;
    }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public decimal? TotalPurchase
    {
      get;
      set;
    }
  }
}
