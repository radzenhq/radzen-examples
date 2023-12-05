using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyApp.Models.Test
{
  public partial class Product
  {
    [NotMapped]
    public string ProductPictureAsString
    {
      get
      {
        return System.Text.Encoding.Default.GetString(ProductPicture);
      }
      set
      {
        ProductPicture = System.Text.Encoding.Default.GetBytes(value);
      }
    }
  }
}
