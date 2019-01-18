using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crm.Models.Crm
{
  [Table("Contacts", Schema = "dbo")]
  public partial class Contact
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }


    [InverseProperty("Contact")]
    public ICollection<Opportunity> Opportunities { get; set; }
    public string Email
    {
      get;
      set;
    }
    public string Company
    {
      get;
      set;
    }
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
    public string Phone
    {
      get;
      set;
    }
  }
}
