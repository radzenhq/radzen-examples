using BlazorMultiTenant.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace BlazorMultiTenant
{
    public partial class Startup
    {
        partial void OnConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(Configuration.GetSection("Multitenancy").Get<Multitenancy>());
        }

        partial void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<SampleContext>())
                {
                    var created = context.Database.EnsureCreated();
                    if (created)
                    {
                        var databaseCreator = (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
                        databaseCreator.CreateTables();
                    }
                }

                using (var context = scope.ServiceProvider.GetService<ApplicationIdentityDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }

    public class Tenant
    {
        public string Name { get; set; }
        public string[] Hostnames { get; set; }
        public string ConnectionString { get; set; }
    }

    public class Multitenancy
    {
        public Collection<Tenant> Tenants { get; set; }
    }
}
