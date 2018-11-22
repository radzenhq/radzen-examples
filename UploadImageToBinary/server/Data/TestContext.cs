using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using MyApp.Models.Test;

namespace MyApp.Data
{
  public partial class TestContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public TestContext(DbContextOptions<TestContext> options):base(options)
    {
    }

    public TestContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        this.OnModelBuilding(builder);
    }


    public DbSet<MyApp.Models.Test.Product> Products
    {
      get;
      set;
    }
  }
}
