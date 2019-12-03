using System.Collections.Generic;
using System.Threading.Tasks;
using Fmas12d.Business.Models;

namespace Fmas12d.Business.Services
{
  public interface IContactDetailTypeService : INameDescriptionBaseService
  {
    Task<IEnumerable<ContactDetailType>> GetAsync(
      int userId,
      bool asNoTracking,
      bool activeOnly
    );

    Task<IEnumerable<ContactDetailType>> GetAsync(
      int assessmentId,
      int userId,
      bool asNoTracking,
      bool activeOnly
    );
  }
}