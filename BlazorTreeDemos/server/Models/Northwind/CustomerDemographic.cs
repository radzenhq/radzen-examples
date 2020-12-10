using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreeDemos.Models.Northwind
{
  [Table("CustomerDemographics", Schema = "dbo")]
  public partial class CustomerDemographic
  {
    [Key]
    public string CustomerTypeID
    {
      get;
      set;
    }

    public ICollection<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }
    public string CustomerDesc
    {
      get;
      set;
    }
  }
}
