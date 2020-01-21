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

    public async Task<ContactDetail> CreateAsync(ContactDetail model)
    {
      Entities.ContactDetail entity = model.MapToEntity();

      entity.Id = 0;
      entity.IsActive = true;

      UpdateModified(entity);

      _context.Add(entity);

      await _context.SaveChangesAsync();

      model = _context.ContactDetails
                      .Where(e => e.Id == entity.Id)
                      .WhereIsActiveOrActiveOnly(true)
                      .AsNoTracking(true)
                      .Select(ContactDetail.ProjectFromEntity)
                      .Single();
      return model;
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

    public async Task<ContactDetail> GetBaseContactDetailTypeForUserAsync(
      int userId,
      bool asNoTracking,
      bool activeOnly)
    {
      ContactDetail contactDetail = await GetByContactDetailTypeUserAsync(
        ContactDetailType.BASE,
        userId,
        asNoTracking,
        activeOnly
      );
      if (contactDetail == null)
      {
        throw new ContactDetailBaseMissingForUserException(userId);
      }
      return contactDetail;
    }

    private async Task<ContactDetail> GetByContactDetailTypeUserAsync(
      int contactDetailTypeId,
      int userId,
      bool asNoTracking,
      bool activeOnly)
    {
      ContactDetail contactDetail = await _context
        .ContactDetails
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