using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlazorMultiTenant.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        [NotMapped]
        public IEnumerable<string> RoleNames { get; set; }

        [IgnoreDataMember]
        public override string PasswordHash { get; set; }

        [IgnoreDataMember, NotMapped]
        public string Password { get; set; }

        [IgnoreDataMember, NotMapped]
        public string ConfirmPassword { get; set; }

        [IgnoreDataMember, NotMapped]
        public string Name 
        { 
            get
            {
                return UserName;
            }
            set
            {
                UserName = value;
            }
        }
    }
}

