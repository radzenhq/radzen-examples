using Microsoft.EntityFrameworkCore;
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
