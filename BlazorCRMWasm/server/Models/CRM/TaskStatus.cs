using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorCrmWasm.Models.Crm
{
  [Table("TaskStatuses", Schema = "dbo")]
  public partial class TaskStatus
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }

    public IEnumerable<Task> Tasks { get; set; }
    public string Name
    {
      get;
      set;
    }
  }
}
