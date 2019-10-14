using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class OrganisationSeeder : SeederBase
  {
    internal void SeedData()
    {
      Organisation organisation;

      if ((organisation = _context
        .Organisations
          .SingleOrDefault(u => u.Name ==
            ORGANISATION_NAME_1)) == null)
      {
        organisation = new Organisation();
        _context.Add(organisation);
      }
      organisation.Description = ORGANISATION_DESCRIPTION_1;
      organisation.IsActive = true;
      organisation.ModifiedAt = _now;
      organisation.ModifiedByUser = GetSystemAdminUser();
      organisation.Name = ORGANISATION_NAME_1;

      if ((organisation = _context
        .Organisations
          .SingleOrDefault(u => u.Name ==
            ORGANISATION_NAME_2)) == null)
      {
        organisation = new Organisation();
        _context.Add(organisation);
      }
      organisation.Description = ORGANISATION_DESCRIPTION_2;
      organisation.IsActive = true;
      organisation.ModifiedAt = _now;
      organisation.ModifiedByUser = GetSystemAdminUser();
      organisation.Name = ORGANISATION_NAME_2;

      if ((organisation = _context
        .Organisations
          .SingleOrDefault(u => u.Name ==
            ORGANISATION_NAME_3)) == null)
      {
        organisation = new Organisation();
        _context.Add(organisation);
      }
      organisation.Description = ORGANISATION_DESCRIPTION_3;
      organisation.IsActive = true;
      organisation.ModifiedAt = _now;
      organisation.ModifiedByUser = GetSystemAdminUser();
      organisation.Name = ORGANISATION_NAME_3;

      if ((organisation = _context
        .Organisations
          .SingleOrDefault(u => u.Name ==
            ORGANISATION_NAME_4)) == null)
      {
        organisation = new Organisation();
        _context.Add(organisation);
      }
      organisation.Description = ORGANISATION_DESCRIPTION_4;
      organisation.IsActive = true;
      organisation.ModifiedAt = _now;
      organisation.ModifiedByUser = GetSystemAdminUser();
      organisation.Name = ORGANISATION_NAME_4;
    }
  }
}