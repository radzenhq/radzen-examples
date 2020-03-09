using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  [Table("CustOrdersOrders", Schema = "dbo")]
  public partial class CustOrdersOrder
  {
    public int OrderID
    {
      get;
      set;
    }
    public DateTime? OrderDate
    {
      get;
      set;
    }
    public DateTime? RequiredDate
    {
      get;
      set;
    }
    public DateTime? ShippedDate
    {
      get;
      set;
    }
  }
}
