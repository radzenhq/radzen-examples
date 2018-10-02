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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.SpaServices.AngularCli;

using GetCurrentUser.Data;
using GetCurrentUser.Models;
using GetCurrentUser.Authentication;

namespace GetCurrentUser
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
          options.OutputFormatters.Add(new GetCurrentUser.Data.CsvDataContractSerializerOutputFormatter());
          options.OutputFormatters.Add(new GetCurrentUser.Data.XlsDataContractSerializerOutputFormatter());
      });

      services.AddAuthorization();
      services.AddOData();
      services.AddODataQueryFilter();


      var tokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = TokenProviderOptions.Key,
          ValidateIssuer = true,
          ValidIssuer = TokenProviderOptions.Issuer,
          ValidateAudience = true,
          ValidAudience = TokenProviderOptions.Audience,
          ValidateLifetime = true,
          ClockSkew = TimeSpan.Zero
      };

      services.AddAuthentication(options =>
      {
          options.DefaultScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options =>
      {
          options.Audience = TokenProviderOptions.Audience;
          options.ClaimsIssuer = TokenProviderOptions.Issuer;
          options.TokenValidationParameters = tokenValidationParameters;
          options.SaveToken = true;
      });

      services.AddDbContext<ApplicationIdentityDbContext>(options =>
      {
         options.UseSqlServer(Configuration.GetConnectionString("NorthwindConnection"));
      });

      services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

      services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationPrincipalFactory>();


      services.AddDbContext<GetCurrentUser.Data.NorthwindContext>(options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("NorthwindConnection"));
      });

      OnConfigureServices(services);
    }

    partial void OnConfigure(IApplicationBuilder app);
    partial void OnConfigureOData(ODataConventionModelBuilder builder);

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationIdentityDbContext identityDbContext)
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

          oDataBuilder.EntitySet<GetCurrentUser.Models.Northwind.Category>("Categories");
          oDataBuilder.EntitySet<GetCurrentUser.Models.Northwind.Customer>("Customers");

          var customerCustomerDemo = oDataBuilder.EntitySet<GetCurrentUser.Models.Northwind.CustomerCustomerDemo>("CustomerCustomerDemos");
          customerCustomerDemo.EntityType.HasKey(entity => new {
            entity.CustomerID, entity.CustomerTypeID
          });
          oDataBuilder.EntitySet<GetCurrentUser.Models.Northwind.CustomerDemographic>("CustomerDemographics");
          oDataBuilder.EntitySet<GetCurrentUser.Models.Northwind.Employee>("Employees");

          var employeeTerritory = oDataBuilder.EntitySet<GetCurrentUser.Models.Northwind.EmployeeTerritory>("EmployeeTerritories");
          employeeTerritory.EntityType.HasKey(entity => new {
            entity.EmployeeID, entity.TerritoryID
          });
          oDataBuilder.EntitySet<GetCurrentUser.Models.Northwind.Order>("Orders");

          var orderDetail = oDataBuilder.EntitySet<GetCurrentUser.Models.Northwind.OrderDetail>("OrderDetails");
          orderDetail.EntityType.HasKey(entity => new {
            entity.OrderID, entity.ProductID
          });
          oDataBuilder.EntitySet<GetCurrentUser.Models.Northwind.Product>("Products");
          oDataBuilder.EntitySet<GetCurrentUser.Models.Northwind.Region>("Regions");
          oDataBuilder.EntitySet<GetCurrentUser.Models.Northwind.Shipper>("Shippers");
          oDataBuilder.EntitySet<GetCurrentUser.Models.Northwind.Supplier>("Suppliers");
          oDataBuilder.EntitySet<GetCurrentUser.Models.Northwind.Territory>("Territories");

          this.OnConfigureOData(oDataBuilder);

          oDataBuilder.EntitySet<ApplicationUser>("ApplicationUsers");
          var usersType = oDataBuilder.StructuralTypes.First(x => x.ClrType == typeof(ApplicationUser));
          usersType.AddCollectionProperty(typeof(ApplicationUser).GetProperty("RoleNames"));
          oDataBuilder.EntitySet<IdentityRole>("ApplicationRoles");

          var model = oDataBuilder.GetEdmModel();

          builder.MapODataServiceRoute("odata/Northwind", "odata/Northwind", model);

          builder.MapODataServiceRoute("auth", "auth", model);
      });

      identityDbContext.Database.Migrate();

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
