using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditTrail.Models.AuditTrailDb
{
  [Table("Categories", Schema = "dbo")]
  public partial class Category
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id
    {
      get;
      set;
    }
    public string CategoryName
    {
      get;
      set;
    }
    public DateTime? CreatedAt
    {
      get;
      set;
    }
    public DateTime? UpdatedAt
    {
      get;
      set;
    }
    public string CreatedBy
    {
      get;
      set;
    }
    public string UpdatedBy
    {
      get;
      set;
    }
  }
}
