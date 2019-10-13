using Mep.Business.Migrations.Seeds;
using Mep.Business;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;

namespace Mep.Api.Extensions
{
  public static class WebHostExtenstions
  {

    public enum SeedType
    {
      All = 1,
      AllNoGp = 2,
      Test = 3
    }

    public static IWebHost SeedData(this IWebHost host, SeedType seedType)
    {
      using var scope = host.Services.CreateScope();
      IServiceProvider services = scope.ServiceProvider;
      ApplicationContext context = services.GetRequiredService<ApplicationContext>();
      IConfiguration config = services.GetRequiredService<IConfiguration>();

      // now we have the DbContext. Run migrations
      context.Database.Migrate();

      // now that the database is up to date. Let's seed
      switch (seedType)
      {
        case SeedType.AllNoGp:
          new Seeds(context, config).SeedAll(noGp: true);
          break;

        case SeedType.Test:
          new Seeds(context, config).SeedTestAll();
          break;

        default:
          new Seeds(context, config).SeedAll(noGp: false);
          break;
      }

      return host;
    }
  }
}