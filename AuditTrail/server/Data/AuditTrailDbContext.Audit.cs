using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using AuditTrail.Models.AuditTrailDb;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace AuditTrail.Data
{
    public class Auditing
    {
        private static void SetProperty(Type type, EntityEntry entityEntry, string name, object value)
        {
            var propertyInfo = type.GetProperty(name);

            if (propertyInfo != null)
            {
                entityEntry.Property(name).CurrentValue = value;
            }
        }

        public static void Audit(EntityEntry entityEntry, string userName)
        {
            var type = entityEntry.Entity.GetType();

            if (entityEntry.State == EntityState.Added)
            {
                SetProperty(type, entityEntry, "CreatedBy", userName);
                SetProperty(type, entityEntry, "CreatedAt", DateTime.UtcNow);
            }
            else if (entityEntry.State == EntityState.Modified)
            {
                SetProperty(type, entityEntry, "UpdatedBy", userName);
                SetProperty(type, entityEntry, "UpdatedAt", DateTime.UtcNow);
            }
        }
    }

    public partial class AuditTrailDbContext
    {
        public override int SaveChanges()
        {
            var userName = httpAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

            var auditables = ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach(var auditable in auditables)
            {
                Auditing.Audit(auditable, userName);
            }

            return base.SaveChanges();
        }
    }
}
