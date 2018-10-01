using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpDefaultParameterValue.Models.Northwind
{
  [Table("Employee Sales by Country", Schema = "dbo")]
  public partial class EmployeeSalesByCountry
  {
    public string Country
    {
      get;
      set;
    }
    [Key]
    public string LastName
    {
      get;
      set;
    }
    public string FirstName
    {
      get;
      set;
    }
    public DateTime? ShippedDate
    {
      get;
      set;
    }
    public int OrderID
    {
      get;
      set;
    }
    public decimal? SaleAmount
    {
      get;
      set;
    }
  }
}
