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

using MultiTenantSample.Data;
using Microsoft.AspNetCore.Http;
using System.Collections.ObjectModel;

namespace MultiTenantSample
{
    public partial class Startup
    {
        partial void OnConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(Configuration.GetSection("Multitenancy").Get<Multitenancy>());
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
