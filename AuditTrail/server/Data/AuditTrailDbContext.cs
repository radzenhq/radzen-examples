using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using AuditTrail.Models.AuditTrailDb;

namespace AuditTrail.Data
{
    public partial class AuditTrailDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IHttpContextAccessor httpAccessor;

        public AuditTrailDbContext(IHttpContextAccessor httpAccessor, DbContextOptions<AuditTrailDbContext> options):base(options)
        {
            this.httpAccessor = httpAccessor;
        }

        public AuditTrailDbContext(IHttpContextAccessor httpAccessor)
        {
            this.httpAccessor = httpAccessor;
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            this.OnModelBuilding(builder);
        }


        public DbSet<AuditTrail.Models.AuditTrailDb.Category> Categories
        {
          get;
          set;
        }
    }
}
