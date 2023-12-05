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

using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Hosting;

using MyApp.Data;


namespace MyApp
{
    public partial class Startup
    {
        partial void OnConfigureOData(ODataConventionModelBuilder builder)
        {
            // Expose additional property
            var product = builder.EntitySet<MyApp.Models.Test.Product>("Product");
            product.EntityType.Property(i => i.ProductPictureAsString);
        }
    }
}
