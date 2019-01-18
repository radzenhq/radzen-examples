using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crm.Models.Crm
{
  public partial class Opportunity
  {
    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; }
  }
}
