using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface ILocationDetailService : IServiceBaseNoAutoMapper
  {
    Task<Models.Postcode> GetPostcodeDetailsAsync(string stringPostcode);
    Task<Models.Postcode> GetPostcodeDetailsAsync(Models.Postcode modelPostcode);
  }
}