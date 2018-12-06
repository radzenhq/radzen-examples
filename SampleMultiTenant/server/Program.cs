using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace MultiTenantSample
{
  public class Program
  {
      public static void Main(string[] args)
      {
          var host = BuildWebHost(args);

          host.Run();
      }

      public static IWebHost BuildWebHost(string[] args) =>
          WebHost.CreateDefaultBuilder(args)
              .UseKestrel()
              .UseUrls("http://localhost:5001", "http://localhost:5002")
              .UseStartup<Startup>()
              .Build();
  }
}
