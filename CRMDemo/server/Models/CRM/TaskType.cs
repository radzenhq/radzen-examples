using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crm.Models.Crm
{
  [Table("TaskTypes", Schema = "dbo")]
  public partial class TaskType
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }


    [InverseProperty("TaskType")]
    public ICollection<Task> Tasks { get; set; }
    public string Name
    {
      get;
      set;
    }
  }
}
