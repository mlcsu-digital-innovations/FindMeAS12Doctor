using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class AssessmentDetailTypesSeeder : SeederBase<AssessmentDetailType>
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
        AssessmentDetailType.AGRESSIVE_NEIGHBOUR,
        NAME_AGRESSIVE_NEIGHBOUR,
        DESCRIPTION_AGRESSIVE_NEIGHBOUR
      ); 

      AddOrUpdateNameDescriptionEntityById(
        AssessmentDetailType.DANGEROUS_ANIMAL,
        NAME_DANGEROUS_ANIMAL,
        DESCRIPTION_DANGEROUS_ANIMAL
      );

      AddOrUpdateNameDescriptionEntityById(
        AssessmentDetailType.DIFFICULT_PARKING,
        NAME_DIFFICULT_PARKING,
        DESCRIPTION_DIFFICULT_PARKING
      );

      SaveChangesWithIdentity();          
    }
  }
}