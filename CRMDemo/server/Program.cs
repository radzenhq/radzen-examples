using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace Crm
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
              .UseUrls("http://localhost:5000")
              .UseStartup<Startup>()
              .Build();
  }
}
