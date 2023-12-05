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
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    partial void OnConfigureServices(IServiceCollection services);

    partial void OnConfiguringServices(IServiceCollection services);

    public void ConfigureServices(IServiceCollection services)
    {
      OnConfiguringServices(services);

      services.AddOptions();
      services.AddLogging(logging =>
      {
          logging.AddConsole();
          logging.AddDebug();
      });

      services.AddCors(options =>
      {
          options.AddPolicy(
              "AllowAny",
              x =>
              {
                  x.AllowAnyHeader()
                  .AllowAnyMethod()
                  .SetIsOriginAllowed(isOriginAllowed: _ => true)
                  .AllowCredentials();
              });
      });
      services.AddMvc(options =>
      {
          options.EnableEndpointRouting = false;

          options.OutputFormatters.Add(new MyApp.Data.XlsDataContractSerializerOutputFormatter());
          options.FormatterMappings.SetMediaTypeMappingForFormat("xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

          options.OutputFormatters.Add(new MyApp.Data.CsvDataContractSerializerOutputFormatter());
          options.FormatterMappings.SetMediaTypeMappingForFormat("csv", "text/csv");
      }).AddNewtonsoftJson();

      services.AddAuthorization();
      
              var oDataBuilder = new ODataConventionModelBuilder();

              oDataBuilder.EntitySet<MyApp.Models.Test.Product>("Products");

          this.OnConfigureOData(oDataBuilder);


          var model = oDataBuilder.GetEdmModel();
          services.AddControllers().AddOData(opt => {
            opt.AddRouteComponents("odata/Test", model).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
          });


      services.AddHttpContextAccessor();

      services.AddDbContext<MyApp.Data.TestContext>(options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("TestConnection"));
      });

      OnConfigureServices(services);
    }

    partial void OnConfigure(IApplicationBuilder app);


    partial void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env);
    partial void OnConfiguring(IApplicationBuilder app, IWebHostEnvironment env);
    partial void OnConfigureOData(ODataConventionModelBuilder builder);

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

      OnConfiguring(app, env);

      IServiceProvider provider = app.ApplicationServices.GetRequiredService<IServiceProvider>();
      app.UseCors("AllowAny");
      app.Use(async (context, next) => {
          if (context.Request.Path.Value == "/__ssrsreport" || context.Request.Path.Value == "/ssrsproxy") {
            await next();
            return;
          }
          await next();
          if ((context.Response.StatusCode == 404 || context.Response.StatusCode == 401) && !Path.HasExtension(context.Request.Path.Value) && !context.Request.Path.Value.Contains("/odata")) {
              context.Request.Path = "/index.html";
              await next();
          }
      });

      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseDeveloperExceptionPage();

      app.UseRouting();
      app.UseEndpoints(endpoints =>
      {
          endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

          endpoints.MapControllers();
          endpoints.MapFallbackToFile("index.html");
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
      OnConfigure(app, env);
    }
  }

  
  public class BlankTriggerAddingConvention : Microsoft.EntityFrameworkCore.Metadata.Conventions.IModelFinalizingConvention
  {
      public virtual void ProcessModelFinalizing(
          Microsoft.EntityFrameworkCore.Metadata.Builders.IConventionModelBuilder modelBuilder,
          Microsoft.EntityFrameworkCore.Metadata.Conventions.IConventionContext<Microsoft.EntityFrameworkCore.Metadata.Builders.IConventionModelBuilder> context)
      {
          foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
          {
              var table = Microsoft.EntityFrameworkCore.Metadata.StoreObjectIdentifier.Create(entityType, Microsoft.EntityFrameworkCore.Metadata.StoreObjectType.Table);
              if (table != null
                  && entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(table.Value) == null))
              {
                  entityType.Builder.HasTrigger(table.Value.Name + "_Trigger");
              }

              foreach (var fragment in entityType.GetMappingFragments(Microsoft.EntityFrameworkCore.Metadata.StoreObjectType.Table))
              {
                  if (entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(fragment.StoreObject) == null))
                  {
                      entityType.Builder.HasTrigger(fragment.StoreObject.Name + "_Trigger");
                  }
              }
          }
      }
  }
}
