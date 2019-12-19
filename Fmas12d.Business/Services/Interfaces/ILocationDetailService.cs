using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface ILocationDetailService : IServiceBase
  {
    Task<Models.Location> GetPostcodeDetailsAsync(string stringPostcode);
    Task<Models.Location> GetPostcodeDetailsAsync(Models.Location modelPostcode);
    Task<Models.SearchModels.PostcodeIoSearchResult> SearchPostcodeAsync(string stringPostcode);
  }
}