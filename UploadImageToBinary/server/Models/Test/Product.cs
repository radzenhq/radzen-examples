using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models.Test
{
  [Table("Products", Schema = "dbo")]
  public partial class Product
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }
    public string ProductName
    {
      get;
      set;
    }
    public decimal ProductPrice
    {
      get;
      set;
    }
    public Byte[] ProductPicture
    {
      get;
      set;
    }
  }
}
