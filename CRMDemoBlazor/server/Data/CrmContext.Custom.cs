using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using RadzenCrm.Models;

namespace RadzenCrm.Data
{
    public partial class CrmContext
    {
         partial void OnModelBuilding(ModelBuilder builder)
         {
             builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
         }
    }
}
