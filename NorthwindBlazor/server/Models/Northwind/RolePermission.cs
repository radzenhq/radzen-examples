using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindBlazor.Models.Northwind
{
  [Table("RolePermissions", Schema = "dbo")]
  public partial class RolePermission
  {
    [Key]
    public string RoleName
    {
      get;
      set;
    }
    public string PermissionId
    {
      get;
      set;
    }
  }
}
