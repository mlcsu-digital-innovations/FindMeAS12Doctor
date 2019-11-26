using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface ILocationDetailService : IServiceBaseNoAutoMapper
  {
    Task<Models.Location> GetPostcodeDetailsAsync(string stringPostcode);
    Task<Models.Location> GetPostcodeDetailsAsync(Models.Location modelPostcode);
  }
}