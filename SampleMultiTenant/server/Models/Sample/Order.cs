using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiTenantSample.Models.Sample
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


    [InverseProperty("Order")]
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
