using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace AuditTrail
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
              .UseStartup<Startup>()
              .Build();
  }
}
