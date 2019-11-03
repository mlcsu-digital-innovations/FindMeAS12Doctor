using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface IGpPracticeService : ISearchService
  {
    Task<int?> GetCcgIdById(int id);
  }
}