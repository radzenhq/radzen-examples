using Microsoft.EntityFrameworkCore;
using CRMBlazorServerRBS.Models;

namespace CRMBlazorServerRBS.Data
{
    public partial class RadzenCRMContext
    {
        partial void OnModelBuilding(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        }
    }
}