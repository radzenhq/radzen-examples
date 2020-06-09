using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorCrmWasm.Models.Crm
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

    public IEnumerable<Opportunity> Opportunities { get; set; }
    public string Name
    {
      get;
      set;
    }
  }
}
