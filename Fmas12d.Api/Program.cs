using System;
using Fmas12d.Api.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Fmas12d.Api
{
  public static class Program
  {
    public static int Main(string[] args)
    {

      try
      {
        Log.Information("Starting web host");
        IWebHost host = CreateWebHostBuilder(args).Build();

        if (args.Length > 0)
        {
          if (args[0] == "/seed")
          {
            Log.Information("Seeding database without test data");
            host.SeedData(WebHostExtenstions.SeedType.All);
          }
          else if (args[0] == "/seednogppractice")
          {
            Log.Information("Seeding database without GP practice data or test data");
            host.SeedData(WebHostExtenstions.SeedType.AllNoGpPractice);
          }
          else if (args[0] == "/seednogppracticeorccg")
          {
            Log.Information("Seeding database without GP practice or CCG data or test data");
            host.SeedData(WebHostExtenstions.SeedType.AllNoGpPracticeOrCcgs);
          }
          else if (args[0] == "/seedtest")
          {
            Log.Information("Seeding database with test data");
            host.SeedData(WebHostExtenstions.SeedType.Test);
          }
          else if (args[0] == "/removeseed")
          {
            Log.Information("Removing all seed data from database");
            host.SeedData(WebHostExtenstions.SeedType.RemoveAll);
          }
          else if (args[0] == "/removeseedexceptgppractice")
          {
            Log.Information("Removing all seed data except GP practices from database");
            host.SeedData(WebHostExtenstions.SeedType.RemoveAllNoGpPractice);
          }
          else if (args[0] == "/removeseedexceptgppracticeandccg")
          {
            Log.Information("Removing all seed data except GP practices and CCGs from database");
            host.SeedData(WebHostExtenstions.SeedType.RemoveAllNoGpPracticeOrCcgs);
          }
          else if (args[0] == "/removeseedtest")
          {
            Log.Information("Removing test data from database");
            host.SeedData(WebHostExtenstions.SeedType.RemoveTest);
          }
          else
          {
            if (args[0] != "/help" || args[0] != "/h")
            {
              Log.Warning(
                "Unknown argument {CommandLineArgument}",
                args[0]);
            }
            Console.WriteLine(
              "Available arguments:" + Environment.NewLine +
              "  Adding Seeds:" + Environment.NewLine +
              "    /seed: Seed database without Test data" + Environment.NewLine +
              "    /seednogppractice: Seed database without GP practice data or Test data" + Environment.NewLine +
              "    /seednogppracticeorccg Seed database without GP practice or CCG data or Test data" + Environment.NewLine +
              "    /seedtest: Seed database with Test data" + Environment.NewLine +
              "  Removing Seeds:" + Environment.NewLine +
              "    /removeseed: Remove all seed data from database" + Environment.NewLine +
              "    /removeseedexceptgppractice: Remove all seed data except GP practices from database" + Environment.NewLine +
              "    /removeseedexceptgppracticeandccg: Remove all seed data except GP practices and CCGs from database" + Environment.NewLine +
              "    /removeseedtest: Remove Test data from database"
            );
          }
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
