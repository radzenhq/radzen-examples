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

using DataContextAdditionalConfiguration.Data;

namespace DataContextAdditionalConfiguration
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
          options.OutputFormatters.Add(new DataContextAdditionalConfiguration.Data.CsvDataContractSerializerOutputFormatter());
          options.OutputFormatters.Add(new DataContextAdditionalConfiguration.Data.XlsDataContractSerializerOutputFormatter());
      });

      services.AddAuthorization();
      services.AddOData();
      services.AddODataQueryFilter();


      services.AddDbContext<DataContextAdditionalConfiguration.Data.NorthwindContext>(options =>
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

          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.AlphabeticalListOfProduct>("AlphabeticalListOfProducts");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.Category>("Categories");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.CategorySalesFor1997>("CategorySalesFor1997s");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.CurrentProductList>("CurrentProductLists");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.Customer>("Customers");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.CustomerAndSuppliersByCity>("CustomerAndSuppliersByCities");

          var customerCustomerDemo = oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.CustomerCustomerDemo>("CustomerCustomerDemos");
          customerCustomerDemo.EntityType.HasKey(entity => new {
            entity.CustomerID, entity.CustomerTypeID
          });
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.CustomerDemographic>("CustomerDemographics");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.Employee>("Employees");

          var employeeTerritory = oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.EmployeeTerritory>("EmployeeTerritories");
          employeeTerritory.EntityType.HasKey(entity => new {
            entity.EmployeeID, entity.TerritoryID
          });
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.Invoice>("Invoices");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.Order>("Orders");

          var orderDetail = oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.OrderDetail>("OrderDetails");
          orderDetail.EntityType.HasKey(entity => new {
            entity.OrderID, entity.ProductID
          });
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.OrderDetailsExtended>("OrderDetailsExtendeds");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.OrderSubtotal>("OrderSubtotals");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.OrdersQry>("OrdersQries");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.Product>("Products");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.ProductSalesFor1997>("ProductSalesFor1997s");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.ProductsAboveAveragePrice>("ProductsAboveAveragePrices");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.ProductsByCategory>("ProductsByCategories");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.QuarterlyOrder>("QuarterlyOrders");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.Region>("Regions");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.SalesByCategory>("SalesByCategories");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.SalesTotalsByAmount>("SalesTotalsByAmounts");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.Shipper>("Shippers");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.SummaryOfSalesByQuarter>("SummaryOfSalesByQuarters");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.SummaryOfSalesByYear>("SummaryOfSalesByYears");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.Supplier>("Suppliers");
          oDataBuilder.EntitySet<DataContextAdditionalConfiguration.Models.Northwind.Territory>("Territories");

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
