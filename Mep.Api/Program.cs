using System;
using Mep.Api.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Mep.Api
{
  public static class Program
  {
    public static int Main(string[] args)
    {


      try
      {
        Log.Information("Starting web host");
        IWebHost host = CreateWebHostBuilder(args).Build();

        if (args.Length > 0 && args[0] == "/seed")
        {
          Log.Information("Seeding database");
          host.SeedData();
        }
        else
        {
          Log.Information("Running web host");
          host.Run();
        }

        return 0;
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "Host terminated unexpectedly");
        return 1;
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
      WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseSerilog((ctx, cfg) =>
          cfg.ReadFrom.Configuration(ctx.Configuration));
  }
}
