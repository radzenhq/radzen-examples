using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadzenCrm.Models.Crm
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
    public TaskType TaskType { get; set; }
    public int? StatusId
    {
      get;
      set;
    }
    public TaskStatus TaskStatus { get; set; }
  }
}
