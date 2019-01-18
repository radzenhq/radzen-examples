using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Crm.Models
{
    public partial class ApplicationUser
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Picture {get; set;}
    }
}