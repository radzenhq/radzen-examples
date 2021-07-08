using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomSecurity.Models.CustomSecurityDb
{
  [Table("UserRoles", Schema = "dbo")]
  public partial class UserRole
  {
    [Key]
    public int UserId
    {
      get;
      set;
    }
    public User User { get; set; }
    [Key]
    public int RoleId
    {
      get;
      set;
    }
    public Role Role { get; set; }
  }
}
