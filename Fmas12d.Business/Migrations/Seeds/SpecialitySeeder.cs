using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class SpecialitiesSeeder : SeederBase<Speciality>
  {
    #region Constants
    internal const string DESCRIPTION_ADULT_MH = "Adult MH Description";
    internal const string DESCRIPTION_CHILDREN = "Children Description";
    internal const string DESCRIPTION_LEARNING_DISABILITY = "Learning Disability Description";    
    internal const string DESCRIPTION_NEUROPSYCHOLOGICAL = "Neuropsychological Description";
    internal const string DESCRIPTION_OLDER_PEOPLE_MH = "Older People MH Description";
    internal const string NAME_ADULT_MH = "Adult MH";
    internal const string NAME_CHILDREN = "Children";
    internal const string NAME_LEARNING_DISABILITY = "Learning Disability";    
    internal const string NAME_NEUROPSYCHOLOGICAL = "Neuropsychological";
    internal const string NAME_OLDER_PEOPLE_MH = "Older People MH";    
    #endregion
    
    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.Speciality.ADULT_MH,
        NAME_ADULT_MH,
        DESCRIPTION_ADULT_MH
      ); 

      AddOrUpdateNameDescriptionEntityById(
        Models.Speciality.CHILDREN,
        NAME_CHILDREN,
        DESCRIPTION_CHILDREN
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.Speciality.LEARNING_DISABILITY,
        NAME_LEARNING_DISABILITY,
        DESCRIPTION_LEARNING_DISABILITY
      );      

      AddOrUpdateNameDescriptionEntityById(
        Models.Speciality.NEUROPSYCHOLOGICAL,
        NAME_NEUROPSYCHOLOGICAL,
        DESCRIPTION_NEUROPSYCHOLOGICAL
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.Speciality.OLDER_PEOPLE_MH,
        NAME_OLDER_PEOPLE_MH,
        DESCRIPTION_OLDER_PEOPLE_MH
      );    

      SaveChangesWithIdentity();  
    }
  }
}