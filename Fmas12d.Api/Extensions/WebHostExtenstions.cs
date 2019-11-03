using Fmas12d.Business.Migrations.Seeds;
using Fmas12d.Business;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;

namespace Fmas12d.Api.Extensions
{
  public static class WebHostExtenstions
  {

    public enum SeedType
    {
      All = 1,
      AllNoGpPractice = 2,
      Test = 3,
      RemoveTest = 4,
      AllNoGpPracticeOrCcgs = 5,
      RemoveAll = 6,
      RemoveAllNoGpPractice = 7,
      RemoveAllNoGpPracticeOrCcgs = 8
    }

    public static IWebHost SeedData(this IWebHost host, SeedType seedType)
    {
      using var scope = host.Services.CreateScope();
      IServiceProvider services = scope.ServiceProvider;
      ApplicationContext context = services.GetRequiredService<ApplicationContext>();
      IConfiguration config = services.GetRequiredService<IConfiguration>();

      // now we have the DbContext. Run migrations
      // context.Database.Migrate();

      // now that the database is up to date. Let's seed
      switch (seedType)
      {
        case SeedType.All:
          new Seeds(context, config).SeedAll(noGpPractices: false, noCcgs: false);
          break;

        case SeedType.AllNoGpPractice:
          new Seeds(context, config).SeedAll(noGpPractices: true, noCcgs: false);
          break;

        case SeedType.AllNoGpPracticeOrCcgs:
          new Seeds(context, config).SeedAll(noGpPractices: true, noCcgs: true);
          break;          

        case SeedType.Test:
          new Seeds(context, config).SeedTestAll();
          break;

        case SeedType.RemoveAll:
          new Seeds(context, config).RemoveSeedAll(noGpPractices: false, noCcgs: false);
          break;

        case SeedType.RemoveAllNoGpPractice:
          new Seeds(context, config).RemoveSeedAll(noGpPractices: true, noCcgs: false);
          break;

        case SeedType.RemoveAllNoGpPracticeOrCcgs:
          new Seeds(context, config).RemoveSeedAll(noGpPractices: true, noCcgs: true);
          break;             

        case SeedType.RemoveTest:
          new Seeds(context, config).RemoveSeedTestAll();
          break;
      }

      return host;
    }
  }
}