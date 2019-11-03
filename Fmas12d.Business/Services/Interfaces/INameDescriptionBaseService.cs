using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public interface INameDescriptionBaseService : IServiceBaseNoAutoMapper
  {
    Task<IEnumerable<Models.NameDescription>> GetNameDescriptions(bool asNoTracking = true, bool activeOnly = true);
  }
}