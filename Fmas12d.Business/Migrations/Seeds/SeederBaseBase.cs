using Microsoft.Extensions.Configuration;

namespace Fmas12d.Business.Migrations.Seeds
{
  public class SeederBaseBase
  {
    public static IConfiguration Config { get; set; }
    public static ApplicationContext Context { get; set; }
  }
}