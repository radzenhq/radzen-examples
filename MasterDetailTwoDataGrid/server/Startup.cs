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

using MasterDetailTwoDataGrid.Data;

namespace MasterDetailTwoDataGrid
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
      services.AddOptions();
      services.AddCors();

      services.AddMvc(options =>
      {
          options.FormatterMappings.SetMediaTypeMappingForFormat("csv", "text/csv");
          options.OutputFormatters.Add(new MasterDetailTwoDataGrid.Data.CsvDataContractSerializerOutputFormatter());
          options.OutputFormatters.Add(new MasterDetailTwoDataGrid.Data.XlsDataContractSerializerOutputFormatter());
      });

      services.AddAuthorization();
      services.AddOData();
      services.AddODataQueryFilter();


      services.AddDbContext<MasterDetailTwoDataGrid.Data.NorthwindContext>(options =>
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

          oDataBuilder.EntitySet<MasterDetailTwoDataGrid.Models.Northwind.Category>("Categories");
          oDataBuilder.EntitySet<MasterDetailTwoDataGrid.Models.Northwind.Customer>("Customers");

          var customerCustomerDemo = oDataBuilder.EntitySet<MasterDetailTwoDataGrid.Models.Northwind.CustomerCustomerDemo>("CustomerCustomerDemos");
          customerCustomerDemo.EntityType.HasKey(entity => new {
            entity.CustomerID, entity.CustomerTypeID
          });
          oDataBuilder.EntitySet<MasterDetailTwoDataGrid.Models.Northwind.CustomerDemographic>("CustomerDemographics");
          oDataBuilder.EntitySet<MasterDetailTwoDataGrid.Models.Northwind.Employee>("Employees");

          var employeeTerritory = oDataBuilder.EntitySet<MasterDetailTwoDataGrid.Models.Northwind.EmployeeTerritory>("EmployeeTerritories");
          employeeTerritory.EntityType.HasKey(entity => new {
            entity.EmployeeID, entity.TerritoryID
          });
          oDataBuilder.EntitySet<MasterDetailTwoDataGrid.Models.Northwind.Order>("Orders");

          var orderDetail = oDataBuilder.EntitySet<MasterDetailTwoDataGrid.Models.Northwind.OrderDetail>("OrderDetails");
          orderDetail.EntityType.HasKey(entity => new {
            entity.OrderID, entity.ProductID
          });
          oDataBuilder.EntitySet<MasterDetailTwoDataGrid.Models.Northwind.Product>("Products");
          oDataBuilder.EntitySet<MasterDetailTwoDataGrid.Models.Northwind.Region>("Regions");
          oDataBuilder.EntitySet<MasterDetailTwoDataGrid.Models.Northwind.Shipper>("Shippers");
          oDataBuilder.EntitySet<MasterDetailTwoDataGrid.Models.Northwind.Supplier>("Suppliers");
          oDataBuilder.EntitySet<MasterDetailTwoDataGrid.Models.Northwind.Territory>("Territories");

          this.OnConfigureOData(oDataBuilder);

          var model = oDataBuilder.GetEdmModel();

          builder.MapODataServiceRoute("odata/Northwind", "odata/Northwind", model);

      });


      if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("RADZEN")) && env.IsDevelopment())
      {
        app.UseSpa(spa =>
        {
          spa.Options.SourcePath = "../client";
          spa.UseAngularCliServer(npmScript: "start -- --port 8000 --open");
        });
      }

      OnConfigure(app);
    }
  }
}
