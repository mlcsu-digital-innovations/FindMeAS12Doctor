using System.Threading.Tasks;
using Fmas12d.Business.Models;

namespace Fmas12d.Business.Services
{
  public interface IUserAvailabilityService : IServiceBaseNoAutoMapper
  {
    Task<IUserAvailability> Create(IUserAvailability model);
  }
}