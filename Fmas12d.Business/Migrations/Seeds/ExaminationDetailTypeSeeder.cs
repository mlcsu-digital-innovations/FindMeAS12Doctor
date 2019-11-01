using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class ExaminationDetailTypesSeeder : SeederBase<ExaminationDetailType>
  {
    #region Constants
    internal const string DESCRIPTION_AGRESSIVE_NEIGHBOUR =
      "There is an agressive neighbour at the location";
    internal const string DESCRIPTION_DANGEROUS_ANIMAL =
      "A dangerous animal has been reported to be present on the premises";
    internal const string DESCRIPTION_DIFFICULT_PARKING =
      "Parking is difficult at the location";
    internal const string NAME_AGRESSIVE_NEIGHBOUR = "Agressive neighbour";
    internal const string NAME_DANGEROUS_ANIMAL = "Dangerous animal";
    internal const string NAME_DIFFICULT_PARKING = "Parking is difficult";    
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        ExaminationDetailType.AGRESSIVE_NEIGHBOUR,
        NAME_AGRESSIVE_NEIGHBOUR,
        DESCRIPTION_AGRESSIVE_NEIGHBOUR
      ); 

      AddOrUpdateNameDescriptionEntityById(
        ExaminationDetailType.DANGEROUS_ANIMAL,
        NAME_DANGEROUS_ANIMAL,
        DESCRIPTION_DANGEROUS_ANIMAL
      );

      AddOrUpdateNameDescriptionEntityById(
        ExaminationDetailType.DIFFICULT_PARKING,
        NAME_DIFFICULT_PARKING,
        DESCRIPTION_DIFFICULT_PARKING
      );

      SaveChangesWithIdentity();          
    }
  }
}