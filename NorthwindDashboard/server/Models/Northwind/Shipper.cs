using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Models.Northwind
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
