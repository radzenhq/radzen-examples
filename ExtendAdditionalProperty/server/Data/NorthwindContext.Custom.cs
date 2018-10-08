using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using ExtendAdditionalProperty.Models.Northwind;

namespace ExtendAdditionalProperty.Data
{
    public partial class NorthwindContext
    {
        partial void OnModelBuilding(ModelBuilder builder)
        {
            builder.Entity<ExtendAdditionalProperty.Models.Northwind.OrderDetailsExtended>().HasKey(table => new
            {
                table.OrderID,
                table.ProductID
            });
        }
    }
}
