using Mep.Business.Migrations.Seeds;
using Mep.Business;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mep.Api.Extensions
{
  public static class WebHostExtenstions
  {
    public static IWebHost SeedData(this IWebHost host)
    {
      using (var scope = host.Services.CreateScope())
      {
        IServiceProvider services = scope.ServiceProvider;
        ApplicationContext context = services.GetRequiredService<ApplicationContext>();

        // now we have the DbContext. Run migrations
        context.Database.Migrate();

        // now that the database is up to date. Let's seed
        new Seeds(context).SeedAll();

        return host;
      }
    }

    public static IWebHost SeedDataNoGp(this IWebHost host)
    {
      using (var scope = host.Services.CreateScope())
      {
        IServiceProvider services = scope.ServiceProvider;
        ApplicationContext context = services.GetRequiredService<ApplicationContext>();

        // now we have the DbContext. Run migrations
        context.Database.Migrate();

        // now that the database is up to date. Let's seed
        new Seeds(context).SeedAllNoGp();

        return host;
      }
    }

    public static IWebHost SeedTestData(this IWebHost host)
    {
      using (var scope = host.Services.CreateScope())
      {
        IServiceProvider services = scope.ServiceProvider;
        ApplicationContext context = services.GetRequiredService<ApplicationContext>();

        // now we have the DbContext. Run migrations
        context.Database.Migrate();

        // now that the database is up to date. Let's seed
        new TestSeeds(context).TestSeedAll();

        return host;
      }
    }
  }
}