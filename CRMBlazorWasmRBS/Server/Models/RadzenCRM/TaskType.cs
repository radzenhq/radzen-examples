using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CRMBlazorWasmRBS.Server.Models.RadzenCRM
{
    [Table("TaskTypes", Schema = "dbo")]
    public partial class TaskType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Task> Tasks { get; set; }

    }
}