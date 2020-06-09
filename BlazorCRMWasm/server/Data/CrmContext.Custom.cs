using BlazorCrmWasm.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrmWasm.Data
{
    public partial class CrmContext
    {
        partial void OnModelBuilding(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        }
    }
}