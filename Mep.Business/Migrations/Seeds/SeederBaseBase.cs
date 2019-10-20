using Microsoft.Extensions.Configuration;

namespace Mep.Business.Migrations.Seeds
{
  public class SeederBaseBase
  {
    public static IConfiguration _config;
    public static ApplicationContext _context;    
  }
}