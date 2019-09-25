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
          .SingleOrDefault(u => u.Name == ORGANISATION1)) == null)
      {
        organisation = new Organisation();
        _context.Add(organisation);
      }
      organisation.Description = ORGANISATION1;
      organisation.IsActive = true;
      organisation.ModifiedAt = _now;
      organisation.ModifiedByUser = GetSystemAdminUser();
      organisation.Name = ORGANISATION1;

            if ((organisation =
        _context.Organisations
          .SingleOrDefault(u => u.Name == ORGANISATION2)) == null)
      {
        organisation = new Organisation();
        _context.Add(organisation);
      }
      organisation.Description = ORGANISATION2;
      organisation.IsActive = true;
      organisation.ModifiedAt = _now;
      organisation.ModifiedByUser = GetSystemAdminUser();
      organisation.Name = ORGANISATION2;

      if ((organisation =
        _context.Organisations
          .SingleOrDefault(u => u.Name == ORGANISATION3)) == null)
      {
        organisation = new Organisation();
        _context.Add(organisation);
      }
      organisation.Description = ORGANISATION3;
      organisation.IsActive = true;
      organisation.ModifiedAt = _now;
      organisation.ModifiedByUser = GetSystemAdminUser();
      organisation.Name = ORGANISATION3;

      if ((organisation =
        _context.Organisations
          .SingleOrDefault(u => u.Name == ORGANISATION4)) == null)
      {
        organisation = new Organisation();
        _context.Add(organisation);
      }
      organisation.Description = ORGANISATION4;
      organisation.IsActive = true;
      organisation.ModifiedAt = _now;
      organisation.ModifiedByUser = GetSystemAdminUser();
      organisation.Name = ORGANISATION4;
    }
  }
}