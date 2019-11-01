using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class SpecialitiesSeeder : SeederBase<Speciality>
  {
    #region Constants
    internal const string DESCRIPTION_CHILD = "Child Description";
    internal const string DESCRIPTION_LEARNING_DIFFICULTY = "Learning Difficulty Description";
    internal const string NAME_CHILD = "Child";
    internal const string NAME_LEARNING_DIFFICULTY = "Learning Difficulty";
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.Speciality.CHILD,
        NAME_CHILD,
        DESCRIPTION_CHILD
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.Speciality.LEARNING_DIFFICULTY,
        NAME_LEARNING_DIFFICULTY,
        DESCRIPTION_LEARNING_DIFFICULTY
      );    

      SaveChangesWithIdentity();  
    }
  }
}