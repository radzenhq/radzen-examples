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
          options.OutputFormatters.Add(new ExtendAdditionalProperty.Data.CsvDataContractSerializerOutputFormatter());
          options.OutputFormatters.Add(new ExtendAdditionalProperty.Data.XlsDataContractSerializerOutputFormatter());
      });

      services.AddAuthorization();
      services.AddOData();
      services.AddODataQueryFilter();


      services.AddDbContext<ExtendAdditionalProperty.Data.NorthwindContext>(options =>
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

          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.AlphabeticalListOfProduct>("AlphabeticalListOfProducts");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.Category>("Categories");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.CategorySalesFor1997>("CategorySalesFor1997s");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.CurrentProductList>("CurrentProductLists");

          var custOrderHists = oDataBuilder.Function("CustOrderHistsFunc");
          custOrderHists.Parameter<string>("CustomerID");
          custOrderHists.ReturnsCollectionFromEntitySet<ExtendAdditionalProperty.Models.Northwind.CustOrderHist>("CustOrderHists");

          var custOrdersDetails = oDataBuilder.Function("CustOrdersDetailsFunc");
          custOrdersDetails.Parameter<int?>("OrderID");
          custOrdersDetails.ReturnsCollectionFromEntitySet<ExtendAdditionalProperty.Models.Northwind.CustOrdersDetail>("CustOrdersDetails");

          var custOrdersOrders = oDataBuilder.Function("CustOrdersOrdersFunc");
          custOrdersOrders.Parameter<string>("CustomerID");
          custOrdersOrders.ReturnsCollectionFromEntitySet<ExtendAdditionalProperty.Models.Northwind.CustOrdersOrder>("CustOrdersOrders");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.Customer>("Customers");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.CustomerAndSuppliersByCity>("CustomerAndSuppliersByCities");

          var customerCustomerDemo = oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.CustomerCustomerDemo>("CustomerCustomerDemos");
          customerCustomerDemo.EntityType.HasKey(entity => new {
            entity.CustomerID, entity.CustomerTypeID
          });
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.CustomerDemographic>("CustomerDemographics");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.Employee>("Employees");

          var employeeSalesByCountries = oDataBuilder.Function("EmployeeSalesByCountriesFunc");
          employeeSalesByCountries.Parameter<string>("Beginning_Date");
          employeeSalesByCountries.Parameter<string>("Ending_Date");
          employeeSalesByCountries.ReturnsCollectionFromEntitySet<ExtendAdditionalProperty.Models.Northwind.EmployeeSalesByCountry>("EmployeeSalesByCountries");

          var employeeTerritory = oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.EmployeeTerritory>("EmployeeTerritories");
          employeeTerritory.EntityType.HasKey(entity => new {
            entity.EmployeeID, entity.TerritoryID
          });
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.Invoice>("Invoices");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.Order>("Orders");

          var orderDetail = oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.OrderDetail>("OrderDetails");
          orderDetail.EntityType.HasKey(entity => new {
            entity.OrderID, entity.ProductID
          });
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.OrderDetailsExtended>("OrderDetailsExtendeds");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.OrderSubtotal>("OrderSubtotals");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.OrdersQry>("OrdersQries");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.Product>("Products");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.ProductSalesFor1997>("ProductSalesFor1997s");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.ProductsAboveAveragePrice>("ProductsAboveAveragePrices");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.ProductsByCategory>("ProductsByCategories");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.QuarterlyOrder>("QuarterlyOrders");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.Region>("Regions");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.SalesByCategory>("SalesByCategories");

          var salesByCategory1S = oDataBuilder.Function("SalesByCategory1sFunc");
          salesByCategory1S.Parameter<string>("CategoryName");
          salesByCategory1S.Parameter<string>("OrdYear");
          salesByCategory1S.ReturnsCollectionFromEntitySet<ExtendAdditionalProperty.Models.Northwind.SalesByCategory1>("SalesByCategory1s");

          var salesByYears = oDataBuilder.Function("SalesByYearsFunc");
          salesByYears.Parameter<string>("Beginning_Date");
          salesByYears.Parameter<string>("Ending_Date");
          salesByYears.ReturnsCollectionFromEntitySet<ExtendAdditionalProperty.Models.Northwind.SalesByYear>("SalesByYears");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.SalesTotalsByAmount>("SalesTotalsByAmounts");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.Shipper>("Shippers");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.SummaryOfSalesByQuarter>("SummaryOfSalesByQuarters");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.SummaryOfSalesByYear>("SummaryOfSalesByYears");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.Supplier>("Suppliers");

          var tenMostExpensiveProducts = oDataBuilder.Function("TenMostExpensiveProductsFunc");
          tenMostExpensiveProducts.ReturnsCollectionFromEntitySet<ExtendAdditionalProperty.Models.Northwind.TenMostExpensiveProduct>("TenMostExpensiveProducts");
          oDataBuilder.EntitySet<ExtendAdditionalProperty.Models.Northwind.Territory>("Territories");

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
