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
    public static IWebHost SeedData(this IWebHost host, string seed)
    {
      using var scope = host.Services.CreateScope();
      IServiceProvider services = scope.ServiceProvider;
      ApplicationContext context = services.GetRequiredService<ApplicationContext>();

      // now we have the DbContext. Run migrations
      context.Database.Migrate();

      // now that the database is up to date. Let's seed
      if (seed == "seed")
      {
        new Seeds(context).SeedAll();
      }
      else if (seed == "seednogp")
      {
        new Seeds(context).SeedAllNoGp();
      }
      else if (seed == "seedtest")
      {
        new Seeds(context).TestSeedAll();
      }

      return host;
    }
  }
}