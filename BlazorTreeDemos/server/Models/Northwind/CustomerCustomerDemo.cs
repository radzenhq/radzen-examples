using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreeDemos.Models.Northwind
{
  [Table("CustomerCustomerDemo", Schema = "dbo")]
  public partial class CustomerCustomerDemo
  {
    [Key]
    public string CustomerID
    {
      get;
      set;
    }
    public Customer Customer { get; set; }
    [Key]
    public string CustomerTypeID
    {
      get;
      set;
    }
    public CustomerDemographic CustomerDemographic { get; set; }
  }
}
