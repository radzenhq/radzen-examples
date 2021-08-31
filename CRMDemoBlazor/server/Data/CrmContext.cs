using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using RadzenCrm.Models.Crm;

namespace RadzenCrm.Data
{
  public partial class CrmContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public CrmContext(DbContextOptions<CrmContext> options):base(options)
    {
    }

    public CrmContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<RadzenCrm.Models.Crm.Opportunity>()
              .HasOne(i => i.Contact)
              .WithMany(i => i.Opportunities)
              .HasForeignKey(i => i.ContactId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<RadzenCrm.Models.Crm.Opportunity>()
              .HasOne(i => i.OpportunityStatus)
              .WithMany(i => i.Opportunities)
              .HasForeignKey(i => i.StatusId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<RadzenCrm.Models.Crm.Task>()
              .HasOne(i => i.Opportunity)
              .WithMany(i => i.Tasks)
              .HasForeignKey(i => i.OpportunityId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<RadzenCrm.Models.Crm.Task>()
              .HasOne(i => i.TaskType)
              .WithMany(i => i.Tasks)
              .HasForeignKey(i => i.TypeId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<RadzenCrm.Models.Crm.Task>()
              .HasOne(i => i.TaskStatus)
              .WithMany(i => i.Tasks)
              .HasForeignKey(i => i.StatusId)
              .HasPrincipalKey(i => i.Id);


        builder.Entity<RadzenCrm.Models.Crm.Opportunity>()
              .Property(p => p.CloseDate)
              .HasColumnType("datetime");

        builder.Entity<RadzenCrm.Models.Crm.Task>()
              .Property(p => p.DueDate)
              .HasColumnType("datetime");

        builder.Entity<RadzenCrm.Models.Crm.Contact>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<RadzenCrm.Models.Crm.Opportunity>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<RadzenCrm.Models.Crm.Opportunity>()
              .Property(p => p.Amount)
              .HasPrecision(19, 4);

        builder.Entity<RadzenCrm.Models.Crm.Opportunity>()
              .Property(p => p.ContactId)
              .HasPrecision(10, 0);

        builder.Entity<RadzenCrm.Models.Crm.Opportunity>()
              .Property(p => p.StatusId)
              .HasPrecision(10, 0);

        builder.Entity<RadzenCrm.Models.Crm.OpportunityStatus>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<RadzenCrm.Models.Crm.Task>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<RadzenCrm.Models.Crm.Task>()
              .Property(p => p.OpportunityId)
              .HasPrecision(10, 0);

        builder.Entity<RadzenCrm.Models.Crm.Task>()
              .Property(p => p.TypeId)
              .HasPrecision(10, 0);

        builder.Entity<RadzenCrm.Models.Crm.Task>()
              .Property(p => p.StatusId)
              .HasPrecision(10, 0);

        builder.Entity<RadzenCrm.Models.Crm.TaskStatus>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<RadzenCrm.Models.Crm.TaskType>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);
        this.OnModelBuilding(builder);
    }


    public DbSet<RadzenCrm.Models.Crm.Contact> Contacts
    {
      get;
      set;
    }

    public DbSet<RadzenCrm.Models.Crm.Opportunity> Opportunities
    {
      get;
      set;
    }

    public DbSet<RadzenCrm.Models.Crm.OpportunityStatus> OpportunityStatuses
    {
      get;
      set;
    }

    public DbSet<RadzenCrm.Models.Crm.Task> Tasks
    {
      get;
      set;
    }

    public DbSet<RadzenCrm.Models.Crm.TaskStatus> TaskStatuses
    {
      get;
      set;
    }

    public DbSet<RadzenCrm.Models.Crm.TaskType> TaskTypes
    {
      get;
      set;
    }
  }
}
