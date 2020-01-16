using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Models.Northwind
{
  [Table("Territories", Schema = "dbo")]
  public partial class Territory
  {
    [Key]
    public string TerritoryID
    {
      get;
      set;
    }


    public ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }
    public string TerritoryDescription
    {
      get;
      set;
    }
    public int RegionID
    {
      get;
      set;
    }

    public Region Region { get; set; }
  }
}
