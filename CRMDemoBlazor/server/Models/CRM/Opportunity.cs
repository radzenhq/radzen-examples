using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadzenCrm.Models.Crm
{
  [Table("Opportunities", Schema = "dbo")]
  public partial class Opportunity
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }

    public ICollection<Task> Tasks { get; set; }
    public decimal Amount
    {
      get;
      set;
    }
    public string Name
    {
      get;
      set;
    }
    public string UserId
    {
      get;
      set;
    }
    public int ContactId
    {
      get;
      set;
    }
    public Contact Contact { get; set; }
    public int StatusId
    {
      get;
      set;
    }
    public OpportunityStatus OpportunityStatus { get; set; }
    public DateTime CloseDate
    {
      get;
      set;
    }
  }
}
