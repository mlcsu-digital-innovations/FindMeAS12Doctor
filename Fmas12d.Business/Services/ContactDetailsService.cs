using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{

  public class ContactDetailsService :
    ServiceBase<Entities.ContactDetail>, IContactDetailsService
  {
    public ContactDetailsService(
      ApplicationContext context,
      IUserClaimsService userClaimsService)
      : base(context, userClaimsService)
    {

    }

    public async Task<ContactDetail> Get(
      int id,
      int userId,
      bool asNoTracking,
      bool activeOnly)
    {
      ContactDetail contactDetail = await _context
        .ContactDetails
        .Where(c => c.Id == id)
        .WhereIsActiveOrActiveOnly(activeOnly)
        .Where(c => c.UserId == userId)
        .AsNoTracking(asNoTracking)
        .Select(ContactDetail.ProjectFromEntity)
        .SingleOrDefaultAsync();

      return contactDetail;
    }
  }
}