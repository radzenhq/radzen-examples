using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using CustomSecurity.Models.CustomSecurityDb;

namespace CustomSecurity.Data
{
  public partial class CustomSecurityDbContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public CustomSecurityDbContext(DbContextOptions<CustomSecurityDbContext> options):base(options)
    {
    }

    public CustomSecurityDbContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CustomSecurity.Models.CustomSecurityDb.UserRole>().HasKey(table => new {
          table.UserId, table.RoleId
        });
        builder.Entity<CustomSecurity.Models.CustomSecurityDb.UserRole>()
              .HasOne(i => i.User)
              .WithMany(i => i.UserRoles)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<CustomSecurity.Models.CustomSecurityDb.UserRole>()
              .HasOne(i => i.Role)
              .WithMany(i => i.UserRoles)
              .HasForeignKey(i => i.RoleId)
              .HasPrincipalKey(i => i.Id);


        builder.Entity<CustomSecurity.Models.CustomSecurityDb.Role>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<CustomSecurity.Models.CustomSecurityDb.User>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<CustomSecurity.Models.CustomSecurityDb.UserRole>()
              .Property(p => p.UserId)
              .HasPrecision(10, 0);

        builder.Entity<CustomSecurity.Models.CustomSecurityDb.UserRole>()
              .Property(p => p.RoleId)
              .HasPrecision(10, 0);
        this.OnModelBuilding(builder);
    }


    public DbSet<CustomSecurity.Models.CustomSecurityDb.Role> Roles
    {
      get;
      set;
    }

    public DbSet<CustomSecurity.Models.CustomSecurityDb.User> Users
    {
      get;
      set;
    }

    public DbSet<CustomSecurity.Models.CustomSecurityDb.UserRole> UserRoles
    {
      get;
      set;
    }
  }
}
