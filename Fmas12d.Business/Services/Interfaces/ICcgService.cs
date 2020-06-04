using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface ICcgService : ISearchService
  {
    Task<int> GetIdFromShortCode(string shortCode);
  }
}