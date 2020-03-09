using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  [Table("Customer and Suppliers by City", Schema = "dbo")]
  public partial class CustomerAndSuppliersByCity
  {
    public string City
    {
      get;
      set;
    }
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
