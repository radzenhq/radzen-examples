using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UploadFilesBlazor.Models.UploadDb
{
  [Table("Files", Schema = "dbo")]
  public partial class File
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }
    public string Name
    {
      get;
      set;
    }
    public string Path
    {
      get;
      set;
    }
  }
}
