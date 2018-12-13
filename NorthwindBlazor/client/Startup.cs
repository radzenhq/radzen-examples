using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NorthwindBlazor.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ODataService>();
            services.AddSingleton<NorthwindService>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<client.App>("app");
        }
    }
}
