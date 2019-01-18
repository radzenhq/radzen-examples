using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crm.Models.Crm
{
  [Table("Tasks", Schema = "dbo")]
  public partial class Task
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }
    public string Title
    {
      get;
      set;
    }
    public int OpportunityId
    {
      get;
      set;
    }

    [ForeignKey("OpportunityId")]
    public Opportunity Opportunity { get; set; }
    public DateTime DueDate
    {
      get;
      set;
    }
    public int TypeId
    {
      get;
      set;
    }

    [ForeignKey("TypeId")]
    public TaskType TaskType { get; set; }
    public int? StatusId
    {
      get;
      set;
    }

    [ForeignKey("StatusId")]
    public TaskStatus TaskStatus { get; set; }
  }
}
