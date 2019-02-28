using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomSecurity.Models.CustomSecurity
{
  [Table("Roles", Schema = "dbo")]
  public partial class Role
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }


    [InverseProperty("Role")]
    public ICollection<UserRole> UserRoles { get; set; }
    public string Name
    {
      get;
      set;
    }
  }
}
