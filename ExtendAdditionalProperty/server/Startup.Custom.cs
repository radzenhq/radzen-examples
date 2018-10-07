using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.SpaServices.AngularCli;

using ExtendAdditionalProperty.Data;

namespace ExtendAdditionalProperty
{
    public partial class Startup
    {
        partial void OnConfigureOData(ODataConventionModelBuilder builder)
        {
            // Expose additional property
            var orderDetail = builder.EntitySet<ExtendAdditionalProperty.Models.Northwind.OrderDetail>("OrderDetails");
            orderDetail.EntityType.Property(od => od.ExtendedPrice);
        }
    }
}
