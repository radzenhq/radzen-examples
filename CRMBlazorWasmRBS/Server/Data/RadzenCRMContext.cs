using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using CRMBlazorWasmRBS.Server.Models.RadzenCRM;

namespace CRMBlazorWasmRBS.Server.Data
{
    public partial class RadzenCRMContext : DbContext
    {
        public RadzenCRMContext()
        {
        }

        public RadzenCRMContext(DbContextOptions<RadzenCRMContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity>()
              .HasOne(i => i.Contact)
              .WithMany(i => i.Opportunities)
              .HasForeignKey(i => i.ContactId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity>()
              .HasOne(i => i.OpportunityStatus)
              .WithMany(i => i.Opportunities)
              .HasForeignKey(i => i.StatusId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task>()
              .HasOne(i => i.Opportunity)
              .WithMany(i => i.Tasks)
              .HasForeignKey(i => i.OpportunityId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task>()
              .HasOne(i => i.TaskStatus)
              .WithMany(i => i.Tasks)
              .HasForeignKey(i => i.StatusId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task>()
              .HasOne(i => i.TaskType)
              .WithMany(i => i.Tasks)
              .HasForeignKey(i => i.TypeId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity>()
              .Property(p => p.CloseDate)
              .HasColumnType("datetime");

            builder.Entity<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task>()
              .Property(p => p.DueDate)
              .HasColumnType("datetime");
            this.OnModelBuilding(builder);
        }

        public DbSet<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact> Contacts { get; set; }

        public DbSet<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity> Opportunities { get; set; }

        public DbSet<CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus> OpportunityStatuses { get; set; }

        public DbSet<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task> Tasks { get; set; }

        public DbSet<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus> TaskStatuses { get; set; }

        public DbSet<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType> TaskTypes { get; set; }
    }
}