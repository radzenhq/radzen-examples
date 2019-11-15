using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorMultiTenant.Models.Sample
{
  [Table("OrderDetails", Schema = "dbo")]
  public partial class OrderDetail
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }
    public int Quantity
    {
      get;
      set;
    }
    public int? OrderId
    {
      get;
      set;
    }
    public Order Order { get; set; }
    public int? ProductId
    {
      get;
      set;
    }
    public Product Product { get; set; }
  }
}
