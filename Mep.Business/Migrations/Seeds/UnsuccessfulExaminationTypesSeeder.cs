using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class UnsuccessfulExaminationTypesSeeder : SeederBase
  {

    internal UnsuccessfulExaminationTypesSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      UnsuccessfulExaminationType unsuccessfulExaminationType;

      if ((unsuccessfulExaminationType = _context.
        UnsuccessfulExaminationTypes
          .SingleOrDefault(g => g.Name ==
            UNSUCCESSFUL_EXAMINATION_TYPE_NAME)) == null)
      {
        unsuccessfulExaminationType = new UnsuccessfulExaminationType();
        _context.Add(unsuccessfulExaminationType);
      }
      unsuccessfulExaminationType.IsActive = true;
      unsuccessfulExaminationType.ModifiedAt = _now;
      unsuccessfulExaminationType.ModifiedByUser = GetSystemAdminUser();
      unsuccessfulExaminationType.Name = UNSUCCESSFUL_EXAMINATION_TYPE_NAME;
      unsuccessfulExaminationType.Description =
        UNSUCCESSFUL_EXAMINATION_TYPE_DESCRIPTION;
    }
  }
}