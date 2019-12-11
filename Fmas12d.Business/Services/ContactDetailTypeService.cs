using Fmas12d.Business.Extensions;
using Fmas12d.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fmas12d.Business.Services
{
  public class ContactDetailTypeService : 
    NameDescriptionBaseService<Data.Entities.ContactDetailType>,
    IContactDetailTypeService
  {
    public ContactDetailTypeService(
      ApplicationContext context,
      IUserClaimsService userClaimsService)
      : base(context, userClaimsService)
    {
    }

    public async Task<IEnumerable<ContactDetailType>> GetAsync(
      int userId, 
      bool asNoTracking, 
      bool activeOnly
    )
    {
      IEnumerable<ContactDetailType> models = await _context
        .ContactDetailTypes
        .Include(cdt => cdt.ContactDetails)
        .Where(cd => cd.ContactDetails.Any(cd => cd.UserId == userId))
        .WhereIsActiveOrActiveOnly(activeOnly)
        .AsNoTracking(asNoTracking)
        .Select(cd => new ContactDetailType {
          ContactDetails = cd.ContactDetails
            .Where(cd => cd.UserId == userId)
            .Select(cd => new ContactDetail(cd))
            .ToList(),
          Description = cd.Description,
          Id = cd.Id,
          IsActive = cd.IsActive,
          ModifiedAt = cd.ModifiedAt,
          ModifiedByUser = new User(cd.ModifiedByUser),
          ModifiedByUserId = cd.ModifiedByUserId,
          Name = cd.Name          
        })
        .ToListAsync();

      return models;    
    }

    public async Task<IEnumerable<ContactDetailType>> GetAsync(
      int assessmentId, 
      int userId, 
      bool asNoTracking, 
      bool activeOnly
    )
    {
      IEnumerable<ContactDetailType> models = await _context
        .ContactDetailTypes        
        .Include(cdt => cdt.ContactDetails)
        .Where(cdt => cdt.ContactDetails.Any(cd => cd.UserId == userId))
        .Where(cdt => cdt.ContactDetails
                         .Any(cd => cd.Ccg.Assessments
                                          .Any(a => a.Id == assessmentId)))
        .WhereIsActiveOrActiveOnly(activeOnly)
        .AsNoTracking(asNoTracking)
        .Select(cd => new ContactDetailType {
          ContactDetails = cd.ContactDetails
            .Where(cd => cd.UserId == userId)
            .Where(cd => cd.Ccg.Assessments.Any(a => a.Id == assessmentId))
            .Select(cd => new ContactDetail(cd))
            .ToList(),
          Description = cd.Description,
          Id = cd.Id,
          IsActive = cd.IsActive,
          ModifiedAt = cd.ModifiedAt,
          ModifiedByUser = new User(cd.ModifiedByUser),
          ModifiedByUserId = cd.ModifiedByUserId,
          Name = cd.Name          
        })
        .ToListAsync();

      return models;        
    }
  }
}