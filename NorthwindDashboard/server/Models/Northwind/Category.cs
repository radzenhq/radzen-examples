using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Models.Northwind
{
  [Table("Categories", Schema = "dbo")]
  public partial class Category
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryID
    {
      get;
      set;
    }


    public ICollection<Product> Products { get; set; }
    public string CategoryName
    {
      get;
      set;
    }
    public string Description
    {
      get;
      set;
    }
    public Byte[] Picture
    {
      get;
      set;
    }
  }
}
