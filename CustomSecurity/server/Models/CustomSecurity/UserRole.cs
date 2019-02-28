using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomSecurity.Models.CustomSecurity
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

    [ForeignKey("UserId")]
    public User User { get; set; }
    public int RoleId
    {
      get;
      set;
    }

    [ForeignKey("RoleId")]
    public Role Role { get; set; }
  }
}
