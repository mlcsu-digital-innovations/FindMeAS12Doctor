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
          Log.Information("Seeding database without test data");
          host.SeedData(WebHostExtenstions.SeedType.All);
        }
        else if (args.Length > 0 && args[0] == "/seednogppractice")
        {
          Log.Information("Seeding database without GP practice data or test data");
          host.SeedData(WebHostExtenstions.SeedType.AllNoGpPractice);
        }
        else if (args.Length > 0 && args[0] == "/seedtest")
        {
          Log.Information("Seeding database with test data");
          host.SeedData(WebHostExtenstions.SeedType.Test);
        }
        else if (args.Length > 0 && args[0] == "/removeseedtest")
        {
          Log.Information("Removing test data from database");
          host.SeedData(WebHostExtenstions.SeedType.RemoveTest);
        }        
        else if (args.Length > 0)
        {
          Log.Warning(
            "Unknown argument {CommandLineArgument}",
            args[0]);
          Console.WriteLine(
            "Available arguments:" + Environment.NewLine +
            "/seed: Seed database without test data" + Environment.NewLine +
            "/seednogppractice: Seed database without GP practice data or test data" + Environment.NewLine +
            "/seedtest: Seed database with test data" + Environment.NewLine +
            "/removeseedtest: Removing test data from database"
        );
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
