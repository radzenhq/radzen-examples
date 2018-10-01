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

using CustomSecurityPasswordPolicy.Data;
using CustomSecurityPasswordPolicy.Models;
using CustomSecurityPasswordPolicy.Authentication;

namespace CustomSecurityPasswordPolicy
{
    public partial class Startup
    {
        partial void OnConfigureServices(IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 0;
            });
        }
    }
}
