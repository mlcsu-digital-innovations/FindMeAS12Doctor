using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ExaminationDetailTypeSeeder : SeederBase
  {
    internal ExaminationDetailTypeSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      ExaminationDetailType examinationDetailType;

      if ((examinationDetailType =
        _context.ExaminationDetailTypes.SingleOrDefault(
          edt => edt.Id == Data.Entities.ExaminationDetailType.DANGEROUS_ANIMAL)) == null)
      {
        examinationDetailType = new ExaminationDetailType();
        _context.Add(examinationDetailType);
      }
      examinationDetailType.IsActive = true;
      examinationDetailType.ModifiedAt = _now;
      examinationDetailType.ModifiedByUser = GetSystemAdminUser();
      examinationDetailType.Name = EXAMINATION_TYPE_DANGEROUS_ANIMAL_NAME;
      examinationDetailType.Description = EXAMINATION_TYPE_DANGEROUS_ANIMAL_DESCRIPTION;
    }
  }
}