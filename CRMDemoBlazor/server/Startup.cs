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
using RadzenCrm.Data;
using RadzenCrm.Models;
using RadzenCrm.Authentication;
using Radzen;

namespace RadzenCrm
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        partial void OnConfigureServices(IServiceCollection services);

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<HttpClient>(serviceProvider =>
            {

              var uriHelper = serviceProvider.GetRequiredService<NavigationManager>();

              return new HttpClient
              {
                BaseAddress = new Uri(uriHelper.BaseUri)
              };
            });

            services.AddHttpClient();

            services.AddAuthentication();
            services.AddAuthorization();
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CRMConnection"));
            }, ServiceLifetime.Transient);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                  .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
                  ApplicationPrincipalFactory>();

            services.AddScoped<SecurityService>();

            services.AddScoped<CrmService>();

            services.AddDbContext<RadzenCrm.Data.CrmContext>(options =>
            {
              options.UseSqlServer(Configuration.GetConnectionString("CRMConnection"));
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddServerSideBlazor().AddHubOptions(o =>
            {
                o.MaximumReceiveMessageSize = 10 * 1024 * 1024;
            });
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();

            services.AddLocalization();

            var supportedCultures = new[]
            {
                new System.Globalization.CultureInfo("en-US"),
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            OnConfigureServices(services);
        }

        partial void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationIdentityDbContext identityDbContext)
        {
            var supportedCultures = new[]
            {
                new System.Globalization.CultureInfo("en-US"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.Use((ctx, next) =>
                {
                    return next();
                });
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            identityDbContext.Database.Migrate();

            OnConfigure(app, env);
        }
    }

}
