using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContextAdditionalConfiguration.Models.Northwind
{
  [Table("Quarterly Orders", Schema = "dbo")]
  public partial class QuarterlyOrder
  {
    [Key]
    public string CustomerID
    {
      get;
      set;
    }
    public string CompanyName
    {
      get;
      set;
    }
    public string City
    {
      get;
      set;
    }
    public string Country
    {
      get;
      set;
    }
  }
}
