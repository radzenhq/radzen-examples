using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tree.Models.Northwind
{
  [Table("Shippers", Schema = "dbo")]
  public partial class Shipper
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ShipperID
    {
      get;
      set;
    }


    [InverseProperty("Shipper")]
    public ICollection<Order> Orders { get; set; }
    public string CompanyName
    {
      get;
      set;
    }
    public string Phone
    {
      get;
      set;
    }
  }
}
