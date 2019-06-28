using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  public class GetProductDetailResult
  {
    public int returnValue { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public string QuantityPerUnit { get; set; }
  }
}
