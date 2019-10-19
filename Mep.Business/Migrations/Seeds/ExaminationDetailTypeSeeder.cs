using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class ExaminationDetailTypeSeeder : SeederBase<ExaminationDetailType>
  {
    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        ExaminationDetailType.AGRESSIVE_NEIGHBOUR,
        EXAMINATION_TYPE_NAME_AGRESSIVE_NEIGHBOUR,
        EXAMINATION_TYPE_DESCRIPTION_AGRESSIVE_NEIGHBOUR
      ); 

      AddOrUpdateNameDescriptionEntityById(
        ExaminationDetailType.DANGEROUS_ANIMAL,
        EXAMINATION_TYPE_NAME_DANGEROUS_ANIMAL,
        EXAMINATION_TYPE_DESCRIPTION_DANGEROUS_ANIMAL
      );

      AddOrUpdateNameDescriptionEntityById(
        ExaminationDetailType.DIFFICULT_PARKING,
        EXAMINATION_TYPE_NAME_DIFFICULT_PARKING,
        EXAMINATION_TYPE_DESCRIPTION_DIFFICULT_PARKING
      );            
    }
  }
}