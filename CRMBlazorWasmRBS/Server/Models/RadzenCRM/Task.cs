using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CRMBlazorWasmRBS.Server.Models.RadzenCRM
{
    [Table("Tasks", Schema = "dbo")]
    public partial class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        [Required]
        public int OpportunityId { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int TypeId { get; set; }

        public int? StatusId { get; set; }

        public Opportunity Opportunity { get; set; }

        public TaskStatus TaskStatus { get; set; }

        public TaskType TaskType { get; set; }

    }
}