using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ExtendApplicationUser.Models
{
    public partial class ApplicationUser
    {
       public string Country { get; set; }
    }
}

