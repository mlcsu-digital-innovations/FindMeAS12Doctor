using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ExaminationDetailTypeSeeder : SeederBase
  {
    internal void SeedData()
    {
      ExaminationDetailType examinationDetailType;

      if ((examinationDetailType =
        _context.ExaminationDetailTypes.SingleOrDefault(
          edt => edt.Id == ExaminationDetailType.AGRESSIVE_NEIGHBOUR)) == null)
      {
        examinationDetailType = new ExaminationDetailType();
        _context.Add(examinationDetailType);
      }

      PopulateNameDescriptionActiveAndModifiedWithSystemUser(
        examinationDetailType,
        EXAMINATION_TYPE_AGRESSIVE_NEIGHBOUR_NAME,
        EXAMINATION_TYPE_AGRESSIVE_NEIGHBOUR_DESCRIPTION
      );

      if ((examinationDetailType =
        _context.ExaminationDetailTypes.SingleOrDefault(
          edt => edt.Id == ExaminationDetailType.DANGEROUS_ANIMAL)) == null)
      {
        examinationDetailType = new ExaminationDetailType();
        _context.Add(examinationDetailType);
      }
      
      PopulateNameDescriptionActiveAndModifiedWithSystemUser(
        examinationDetailType,
        EXAMINATION_TYPE_DANGEROUS_ANIMAL_NAME,
        EXAMINATION_TYPE_DANGEROUS_ANIMAL_DESCRIPTION
      );

      if ((examinationDetailType =
        _context.ExaminationDetailTypes.SingleOrDefault(
          edt => edt.Id == ExaminationDetailType.DIFFICULT_PARKING)) == null)
      {
        examinationDetailType = new ExaminationDetailType();
        _context.Add(examinationDetailType);
      }
      
      PopulateNameDescriptionActiveAndModifiedWithSystemUser(
        examinationDetailType,
        EXAMINATION_TYPE_DIFFICULT_PARKING_NAME,
        EXAMINATION_TYPE_DIFFICULT_PARKING_DESCRIPTION
      );            
    }
  }
}