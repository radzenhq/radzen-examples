using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using CustomSecurity.Models.CustomSecurity;

namespace CustomSecurity.Data
{
  public partial class CustomSecurityContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public CustomSecurityContext(DbContextOptions<CustomSecurityContext> options):base(options)
    {
    }

    public CustomSecurityContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CustomSecurity.Models.CustomSecurity.UserRole>().HasKey(table => new {
          table.UserId, table.RoleId
        });
        builder.Entity<CustomSecurity.Models.CustomSecurity.UserRole>()
              .HasOne(i => i.User)
              .WithMany(i => i.UserRoles)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<CustomSecurity.Models.CustomSecurity.UserRole>()
              .HasOne(i => i.Role)
              .WithMany(i => i.UserRoles)
              .HasForeignKey(i => i.RoleId)
              .HasPrincipalKey(i => i.Id);

        this.OnModelBuilding(builder);
    }


    public DbSet<CustomSecurity.Models.CustomSecurity.Role> Roles
    {
      get;
      set;
    }

    public DbSet<CustomSecurity.Models.CustomSecurity.User> Users
    {
      get;
      set;
    }

    public DbSet<CustomSecurity.Models.CustomSecurity.UserRole> UserRoles
    {
      get;
      set;
    }
  }
}
