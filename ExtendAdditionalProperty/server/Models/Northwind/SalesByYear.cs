using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtendAdditionalProperty.Models.Northwind
{
  [Table("Sales by Year", Schema = "dbo")]
  public partial class SalesByYear
  {
    public DateTime? ShippedDate
    {
      get;
      set;
    }
    [Key]
    public int OrderID
    {
      get;
      set;
    }
    public decimal? Subtotal
    {
      get;
      set;
    }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string Year
    {
      get;
      set;
    }
  }
}
