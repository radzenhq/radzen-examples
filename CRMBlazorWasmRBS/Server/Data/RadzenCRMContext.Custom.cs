using Microsoft.EntityFrameworkCore;
using CRMBlazorWasmRBS.Server.Models;

namespace CRMBlazorWasmRBS.Server.Data
{
    public partial class RadzenCRMContext
    {
        partial void OnModelBuilding(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        }
    }
}