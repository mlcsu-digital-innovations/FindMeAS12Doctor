using System.Threading.Tasks;

namespace Mep.Business.Services
{
  public interface IGpPracticeService : ISearchService
  {
    Task<int?> GetCcgIdById(int id);
  }
}