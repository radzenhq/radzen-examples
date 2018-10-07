using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtendAdditionalProperty.Models.Northwind
{
  [Table("Customer and Suppliers by City", Schema = "dbo")]
  public partial class CustomerAndSuppliersByCity
  {
    public string City
    {
      get;
      set;
    }
    [Key]
    public string CompanyName
    {
      get;
      set;
    }
    public string ContactName
    {
      get;
      set;
    }
    public string Relationship
    {
      get;
      set;
    }
  }
}
