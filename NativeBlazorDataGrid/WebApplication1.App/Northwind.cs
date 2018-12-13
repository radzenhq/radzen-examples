using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.App.Northwind
{
    public partial class NorthwindContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Initial Catalog=Northwind;Persist Security Info=False;Integrated Security=true;MultipleActiveResultSets=False;Encrypt=false;TrustServerCertificate=true;Connection Timeout=30");
            }
        }

        public DbSet<Product> Products
        {
            get;
            set;
        }
    }

    [Table("Products", Schema = "dbo")]
    public partial class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }
        public int? SupplierID
        {
            get;
            set;
        }

        public int? CategoryID
        {
            get;
            set;
        }
        public string QuantityPerUnit
        {
            get;
            set;
        }
        public decimal? UnitPrice
        {
            get;
            set;
        }
        public Int16? UnitsInStock
        {
            get;
            set;
        }
        public Int16? UnitsOnOrder
        {
            get;
            set;
        }
        public Int16? ReorderLevel
        {
            get;
            set;
        }
        public bool? Discontinued
        {
            get;
            set;
        }
    }
}
