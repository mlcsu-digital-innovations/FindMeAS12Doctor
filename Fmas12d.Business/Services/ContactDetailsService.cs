using Entities = Fmas12d.Data.Entities;
using Fmas12d.Business.Extensions;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Fmas12d.Business.Exceptions;

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

    public async Task<ContactDetail> GetByIdAndUserIdAsync(
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

    public async Task<ContactDetail> GetBaseContactDetailTypeForCcgUserAsync(
      int ccgId,
      int userId,
      bool asNoTracking,
      bool activeOnly)
    {
      ContactDetail contactDetail = await GetByContactDetailTypeCcgUserAsync(
        ContactDetailType.BASE,
        ccgId,
        userId,
        asNoTracking,
        activeOnly
      );
      if (contactDetail == null)
      {
        throw new ContactDetailBaseMissingForCcgUserException(ccgId, userId);
      }
      return contactDetail;
    }

    private async Task<ContactDetail> GetByContactDetailTypeCcgUserAsync(
      int contactDetailTypeId,
      int ccgId,
      int userId,
      bool asNoTracking,
      bool activeOnly)
    {
      ContactDetail contactDetail = await _context
        .ContactDetails
        .Where(c => c.CcgId == ccgId)
        .Where(c => c.ContactDetailTypeId == contactDetailTypeId)        
        .Where(c => c.UserId == userId)
        .WhereIsActiveOrActiveOnly(activeOnly)        
        .AsNoTracking(asNoTracking)
        .Select(ContactDetail.ProjectFromEntity)
        .SingleOrDefaultAsync();

      return contactDetail;
    }    
  }
}