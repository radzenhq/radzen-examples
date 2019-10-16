using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NgSelectDataGridCheckBox.Models.Sample
{
  [Table("OrderDetails", Schema = "dbo")]
  public partial class OrderDetail
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }
    public int Quantity
    {
      get;
      set;
    }
    public int? OrderId
    {
      get;
      set;
    }

    [ForeignKey("OrderId")]
    public Order Order { get; set; }
    public int? ProductId
    {
      get;
      set;
    }

    [ForeignKey("ProductId")]
    public Product Product { get; set; }
  }
}
