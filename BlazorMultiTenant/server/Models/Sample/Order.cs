using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorMultiTenant.Models.Sample
{
  [Table("Orders", Schema = "dbo")]
  public partial class Order
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }


    public ICollection<OrderDetail> OrderDetails { get; set; }
    public string UserName
    {
      get;
      set;
    }
    public DateTime OrderDate
    {
      get;
      set;
    }
  }
}
