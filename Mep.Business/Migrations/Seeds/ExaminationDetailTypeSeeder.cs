using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ExaminationDetailTypeSeeder : SeederBase<ExaminationDetailType>
  {
    internal void SeedData()
    {
      ExaminationDetailType examinationDetailType;

      if ((examinationDetailType =
        _context.ExaminationDetailTypes.Find(ExaminationDetailType.AGRESSIVE_NEIGHBOUR)) == null)
      {
        examinationDetailType = new ExaminationDetailType();
        _context.Add(examinationDetailType);
      }

      PopulateNameDescriptionActiveAndModifiedWithSystemUser(
        examinationDetailType,
        EXAMINATION_TYPE_NAME_AGRESSIVE_NEIGHBOUR,
        EXAMINATION_TYPE_DESCRIPTION_AGRESSIVE_NEIGHBOUR
      );

      if ((examinationDetailType =
        _context.ExaminationDetailTypes.Find(ExaminationDetailType.DANGEROUS_ANIMAL)) == null)
      {
        examinationDetailType = new ExaminationDetailType();
        _context.Add(examinationDetailType);
      }
      
      PopulateNameDescriptionActiveAndModifiedWithSystemUser(
        examinationDetailType,
        EXAMINATION_TYPE_NAME_DANGEROUS_ANIMAL,
        EXAMINATION_TYPE_DESCRIPTION_DANGEROUS_ANIMAL
      );

      if ((examinationDetailType =
        _context.ExaminationDetailTypes.Find(ExaminationDetailType.DIFFICULT_PARKING)) == null)
      {
        examinationDetailType = new ExaminationDetailType();
        _context.Add(examinationDetailType);
      }
      
      PopulateNameDescriptionActiveAndModifiedWithSystemUser(
        examinationDetailType,
        EXAMINATION_TYPE_NAME_DIFFICULT_PARKING,
        EXAMINATION_TYPE_DESCRIPTION_DIFFICULT_PARKING
      );            
    }
  }
}