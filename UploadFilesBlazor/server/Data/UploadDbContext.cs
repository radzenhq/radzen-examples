using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using UploadFilesBlazor.Models.UploadDb;

namespace UploadFilesBlazor.Data
{
  public partial class UploadDbContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public UploadDbContext(DbContextOptions<UploadDbContext> options):base(options)
    {
    }

    public UploadDbContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);



        builder.Entity<UploadFilesBlazor.Models.UploadDb.File>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);
        this.OnModelBuilding(builder);
    }


    public DbSet<UploadFilesBlazor.Models.UploadDb.File> Files
    {
      get;
      set;
    }
  }
}
