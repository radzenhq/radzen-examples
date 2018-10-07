using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtendAdditionalProperty.Models.Northwind
{
  public partial class OrderDetail
  {
    // Additional property
    [NotMapped]
    public decimal? ExtendedPrice
    {
      get;
      set;
    }
  }
}
