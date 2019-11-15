using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorMultiTenant.Models.Sample
{
  [Table("Products", Schema = "dbo")]
  public partial class Product
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }


    public ICollection<OrderDetail> OrderDetails { get; set; }
    public string ProductName
    {
      get;
      set;
    }
    public decimal ProductPrice
    {
      get;
      set;
    }
    public string ProductPicture
    {
      get;
      set;
    }
  }
}
