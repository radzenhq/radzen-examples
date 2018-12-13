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

using Microsoft.AspNetCore.ResponseCompression;
using System.Net.Mime;
using Microsoft.AspNetCore.Blazor.Server;
using NorthwindBlazor.App;

using NorthwindBlazor.Data;

namespace NorthwindBlazor
{
  public partial class Startup
  {
    public Startup(IConfiguration configuration, IHostingEnvironment env)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    partial void OnConfigureServices(IServiceCollection services);

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddServerSideBlazor<App.Startup>();

      services.AddResponseCompression(options =>
      {
          options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
          {
              MediaTypeNames.Application.Octet,
              WasmMediaTypeNames.Application.Wasm,
          });
      });

      services.AddOptions();
      services.AddCors();

      services.AddMvc(options =>
      {
          options.FormatterMappings.SetMediaTypeMappingForFormat("csv", "text/csv");
          options.OutputFormatters.Add(new NorthwindBlazor.Data.CsvDataContractSerializerOutputFormatter());
          options.OutputFormatters.Add(new NorthwindBlazor.Data.XlsDataContractSerializerOutputFormatter());
      });

      services.AddAuthorization();
      services.AddOData();
      services.AddODataQueryFilter();


      services.AddDbContext<NorthwindBlazor.Data.NorthwindContext>(options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("NorthwindConnection"));
      });

      OnConfigureServices(services);
    }

    partial void OnConfigure(IApplicationBuilder app);
    partial void OnConfigureOData(ODataConventionModelBuilder builder);

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      IServiceProvider provider = app.ApplicationServices.GetRequiredService<IServiceProvider>();

      app.UseCors(builder =>
        builder.WithOrigins("*")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .AllowAnyOrigin()
      );

      app.Use(async (context, next) => {
          if (context.Request.Path.Value == "/ssrsreport" || context.Request.Path.Value == "/ssrsproxy") {
            await next();
            return;
          }
          try
          {
              await next();
          }
#pragma warning disable 0168
          catch (Microsoft.AspNetCore.Mvc.Internal.AmbiguousActionException ex) {
#pragma warning restore 0168
              if (!Path.HasExtension(context.Request.Path.Value)) {
                  context.Request.Path = "/index.html";
                  await next();
              }
          }

          if ((context.Response.StatusCode == 404 || context.Response.StatusCode == 401) && !Path.HasExtension(context.Request.Path.Value)) {
              context.Request.Path = "/index.html";
              await next();
          }
      });

      app.UseDefaultFiles();
      app.UseStaticFiles();

      app.UseDeveloperExceptionPage();

      app.UseMvc(builder =>
      {
          builder.Count().Filter().OrderBy().Expand().Select().MaxTop(null).SetTimeZoneInfo(TimeZoneInfo.Utc);

          if (env.EnvironmentName == "Development")
          {
              builder.MapRoute(name: "default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "Home", action = "Index" }
              );
          }

          var oDataBuilder = new ODataConventionModelBuilder(provider);

          oDataBuilder.EntitySet<NorthwindBlazor.Models.Northwind.Category>("Categories");
          oDataBuilder.EntitySet<NorthwindBlazor.Models.Northwind.Customer>("Customers");

          var customerCustomerDemo = oDataBuilder.EntitySet<NorthwindBlazor.Models.Northwind.CustomerCustomerDemo>("CustomerCustomerDemos");
          customerCustomerDemo.EntityType.HasKey(entity => new {
            entity.CustomerID, entity.CustomerTypeID
          });
          oDataBuilder.EntitySet<NorthwindBlazor.Models.Northwind.CustomerDemographic>("CustomerDemographics");
          oDataBuilder.EntitySet<NorthwindBlazor.Models.Northwind.Employee>("Employees");

          var employeeTerritory = oDataBuilder.EntitySet<NorthwindBlazor.Models.Northwind.EmployeeTerritory>("EmployeeTerritories");
          employeeTerritory.EntityType.HasKey(entity => new {
            entity.EmployeeID, entity.TerritoryID
          });
          oDataBuilder.EntitySet<NorthwindBlazor.Models.Northwind.Order>("Orders");

          var orderDetail = oDataBuilder.EntitySet<NorthwindBlazor.Models.Northwind.OrderDetail>("OrderDetails");
          orderDetail.EntityType.HasKey(entity => new {
            entity.OrderID, entity.ProductID
          });
          oDataBuilder.EntitySet<NorthwindBlazor.Models.Northwind.Product>("Products");
          oDataBuilder.EntitySet<NorthwindBlazor.Models.Northwind.Region>("Regions");
          oDataBuilder.EntitySet<NorthwindBlazor.Models.Northwind.Shipper>("Shippers");
          oDataBuilder.EntitySet<NorthwindBlazor.Models.Northwind.Supplier>("Suppliers");
          oDataBuilder.EntitySet<NorthwindBlazor.Models.Northwind.Territory>("Territories");

          this.OnConfigureOData(oDataBuilder);

          var model = oDataBuilder.GetEdmModel();

          builder.MapODataServiceRoute("odata/Northwind", "odata/Northwind", model);

      });


      app.UseResponseCompression();

      //app.UseServerSideBlazor<App.Startup>();
      app.UseSignalR(route => route.MapHub<BlazorHub>(BlazorHub.DefaultPath, options =>
      {
          options.ApplicationMaxBufferSize = 10 * 1024 * 1024;
      })).UseBlazor<App.Startup>();

      OnConfigure(app);
    }
  }
}
