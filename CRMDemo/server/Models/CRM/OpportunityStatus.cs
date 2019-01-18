using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crm.Models.Crm
{
  [Table("OpportunityStatuses", Schema = "dbo")]
  public partial class OpportunityStatus
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }


    [InverseProperty("OpportunityStatus")]
    public ICollection<Opportunity> Opportunities { get; set; }
    public string Name
    {
      get;
      set;
    }
  }
}
