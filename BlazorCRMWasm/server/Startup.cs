using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BlazorCrmWasm.Data;
using BlazorCrmWasm.Models;
using BlazorCrmWasm.Authentication;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
namespace BlazorCrmWasm
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        partial void OnConfigureServices(IServiceCollection services);

        partial void OnConfiguringServices(IServiceCollection services);

        public void ConfigureServices(IServiceCollection services)
        {
            OnConfiguringServices(services);

            services.AddHttpContextAccessor();
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
            services.AddOData();
            services.AddODataQueryFilter();

            services.AddDbContext<ApplicationIdentityDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("CRMConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddRoleStore<RoleStore<IdentityRole, ApplicationIdentityDbContext, string>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationIdentityDbContext>();

            services.AddTransient<IdentityServer4.Services.IProfileService, ProfileService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();


            services.AddDbContext<BlazorCrmWasm.Data.CrmContext>(options =>
            {
              options.UseSqlServer(Configuration.GetConnectionString("CRMConnection"));
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
            OnConfigureServices(services);
        }

        partial void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env);
        partial void OnConfigureOData(ODataConventionModelBuilder builder);
        partial void OnConfiguring(IApplicationBuilder app, IWebHostEnvironment env);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationIdentityDbContext identityDbContext)
        {
            OnConfiguring(app, env);
            if (env.IsDevelopment())
            {
                Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.Use((ctx, next) =>
                {
                    ctx.Request.Scheme = "https";
                    return next();
                });
            }
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            IServiceProvider provider = app.ApplicationServices.GetRequiredService<IServiceProvider>();
            app.UseCors("AllowAny");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");

                endpoints.Count().Filter().OrderBy().Expand().Select().MaxTop(null).SetTimeZoneInfo(TimeZoneInfo.Utc);

                var oDataBuilder = new ODataConventionModelBuilder(provider);

                oDataBuilder.EntitySet<BlazorCrmWasm.Models.Crm.Contact>("Contacts");
                oDataBuilder.EntitySet<BlazorCrmWasm.Models.Crm.Opportunity>("Opportunities");
                oDataBuilder.EntitySet<BlazorCrmWasm.Models.Crm.OpportunityStatus>("OpportunityStatuses");
                oDataBuilder.EntitySet<BlazorCrmWasm.Models.Crm.Task>("Tasks");
                oDataBuilder.EntitySet<BlazorCrmWasm.Models.Crm.TaskStatus>("TaskStatuses");
                oDataBuilder.EntitySet<BlazorCrmWasm.Models.Crm.TaskType>("TaskTypes");

                this.OnConfigureOData(oDataBuilder);

                oDataBuilder.EntitySet<ApplicationUser>("ApplicationUsers");
                var usersType = oDataBuilder.StructuralTypes.First(x => x.ClrType == typeof(ApplicationUser));
                usersType.AddCollectionProperty(typeof(ApplicationUser).GetProperty("RoleNames"));
                oDataBuilder.EntitySet<IdentityRole>("ApplicationRoles");

                var model = oDataBuilder.GetEdmModel();

                endpoints.MapODataRoute("odata", "odata/CRM", model);

                endpoints.MapODataRoute("auth", "auth", model);
            });

            identityDbContext.Database.Migrate();

            OnConfigure(app, env);
        }
    }


    public class ProfileService : IdentityServer4.Services.IProfileService
    {
        private readonly IWebHostEnvironment env;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ProfileService(IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.env = env;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task GetProfileDataAsync(IdentityServer4.Models.ProfileDataRequestContext context)
        {
            var user = await userManager.GetUserAsync(context.Subject);

            var roles = user != null ? await userManager.GetRolesAsync(user) :
                env.IsDevelopment() && context.Subject.Identity.Name == "admin" ?
                    roleManager.Roles.Select(r => r.Name) : Enumerable.Empty<string>();

            context.IssuedClaims.AddRange(roles.Select(r => new System.Security.Claims.Claim(IdentityModel.JwtClaimTypes.Role, r)));
        }

        public Task IsActiveAsync(IdentityServer4.Models.IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}
