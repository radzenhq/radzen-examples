using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using MyApp.Models.Test;

namespace MyApp.Data
{
    public partial class TestContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IHttpContextAccessor httpAccessor;

        public TestContext(IHttpContextAccessor httpAccessor, DbContextOptions<TestContext> options):base(options)
        {
            this.httpAccessor = httpAccessor;
        }

        public TestContext(IHttpContextAccessor httpAccessor)
        {
            this.httpAccessor = httpAccessor;
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            builder.Entity<MyApp.Models.Test.Product>()
                  .Property(p => p.Id)
                  .HasPrecision(10, 0);

            builder.Entity<MyApp.Models.Test.Product>()
                  .Property(p => p.ProductPrice)
                  .HasPrecision(19, 4);
            this.OnModelBuilding(builder);
        }


        public DbSet<MyApp.Models.Test.Product> Products
        {
          get;
          set;
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    }
}
