using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  internal class OrganisationSeeder : SeederBase
  {
    internal OrganisationSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      Organisation organisation;

      if ((organisation =
        _context.Organisations
          .SingleOrDefault(u => u.Name == ORGANISATION_1)) == null)
      {
        organisation = new Organisation();
        _context.Add(organisation);
      }
      organisation.Description = ORGANISATION_1;
      organisation.IsActive = true;
      organisation.ModifiedAt = _now;
      organisation.ModifiedByUser = GetSystemAdminUser();
      organisation.Name = ORGANISATION_1;

            if ((organisation =
        _context.Organisations
          .SingleOrDefault(u => u.Name == ORGANISATION_2)) == null)
      {
        organisation = new Organisation();
        _context.Add(organisation);
      }
      organisation.Description = ORGANISATION_2;
      organisation.IsActive = true;
      organisation.ModifiedAt = _now;
      organisation.ModifiedByUser = GetSystemAdminUser();
      organisation.Name = ORGANISATION_2;

      if ((organisation =
        _context.Organisations
          .SingleOrDefault(u => u.Name == ORGANISATION_3)) == null)
      {
        organisation = new Organisation();
        _context.Add(organisation);
      }
      organisation.Description = ORGANISATION_3;
      organisation.IsActive = true;
      organisation.ModifiedAt = _now;
      organisation.ModifiedByUser = GetSystemAdminUser();
      organisation.Name = ORGANISATION_3;

      if ((organisation =
        _context.Organisations
          .SingleOrDefault(u => u.Name == ORGANISATION_4)) == null)
      {
        organisation = new Organisation();
        _context.Add(organisation);
      }
      organisation.Description = ORGANISATION_4;
      organisation.IsActive = true;
      organisation.ModifiedAt = _now;
      organisation.ModifiedByUser = GetSystemAdminUser();
      organisation.Name = ORGANISATION_4;
    }
  }
}